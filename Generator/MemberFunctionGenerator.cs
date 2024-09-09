﻿using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static LanguageExt.Prelude;

namespace HogWarp.Generator
{
    [Generator]
    public class MemberFunctionGenerator : IIncrementalGenerator
    {
        private const string AttributeName = "HogWarp.Lib.Interop.Attributes.FunctionAttribute";

        public static string GetNamespaceFrom(SyntaxNode s) =>
            s.Parent switch
            {
                NamespaceDeclarationSyntax namespaceDeclarationSyntax => namespaceDeclarationSyntax.Name.ToString(),
                null => string.Empty,
                _ => GetNamespaceFrom(s.Parent)
            };

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var collector = context.SyntaxProvider
               .ForAttributeWithMetadataName(
                   AttributeName,
                   static (node, _) => node is MethodDeclarationSyntax
                   {
                       Parent: ClassDeclarationSyntax, 
                       AttributeLists.Count: > 0
                   },
                   static (context, _) =>
                   {
                       ClassDeclarationSyntax classSyntax = (ClassDeclarationSyntax)context.TargetNode.Parent!;

                       MethodDeclarationSyntax methodSyntax = (MethodDeclarationSyntax)context.TargetNode;
                       IMethodSymbol methodSymbol = (IMethodSymbol)context.TargetSymbol;

                       return (classSyntax, methodSymbol);
                   });

            var groupedStructInfoWithMemberInfos =
                collector.Collect().SelectMany(static (item, _) =>
                    item.GroupBy(static items => items.classSyntax, static items => items.methodSymbol,
                    (key, g) => (Key: key, Functions: g.ToList())));

            context.RegisterSourceOutput(groupedStructInfoWithMemberInfos,
            (sourceContext, data) =>
            {
                IndentedStringBuilder builder = new IndentedStringBuilder();

                builder.AppendLine("// autogen");
                builder.AppendLine("using System.Runtime.InteropServices;");

                var klass = data.Key;
                var ns = GetNamespaceFrom(klass);
                var klassName = klass.Identifier.Value;

                builder.AppendLine($"namespace {ns}");
                builder.AppendLine("{");
                builder.IncrementIndent();
                builder.AppendLine($"public unsafe partial class {klassName}");
                builder.AppendLine("{");
                builder.IncrementIndent();

                builder.AppendLine("#pragma warning disable CS0649");
                builder.AppendLine("[StructLayout(LayoutKind.Sequential)]");
                builder.AppendLine("internal struct InitializationFunctionParameters");
                builder.AppendLine("{");
                builder.IncrementIndent();
                foreach (var f in data.Functions)
                {
                    builder.AppendLine($"internal IntPtr {f.Name};");
                }
                builder.DecrementIndent();
                builder.AppendLine("}");
                builder.AppendLine("#pragma warning restore CS0649");
                builder.AppendLine("");

                

                foreach (var f in data.Functions)
                {
                    builder.AppendLine("[UnmanagedFunctionPointer(CallingConvention.ThisCall)]");
                    if (f.ReturnType.OriginalDefinition.ToString() == "string")
                        builder.AppendLines("[return: MarshalAs(UnmanagedType.LPStr)]");

                    var func = $"internal delegate {f.ReturnType.OriginalDefinition} {f.Name}Delegate(IntPtr This";

                    foreach(var p in f.Parameters)
                    {
                        func += ", ";

                        if (p.Type.Name == "String")
                            func += "[MarshalAs(UnmanagedType.LPUTF8Str)] ";

                        func += p.OriginalDefinition;
                    }
                    func = func + ");";

                    builder.AppendLine(func);
                    builder.AppendLine($"static internal {f.Name}Delegate {f.Name}Internal;");
                    builder.AppendLine("");
                }

                builder.AppendLine($"internal static void Initialize(InitializationFunctionParameters Params)");
                builder.AppendLine("{");
                builder.IncrementIndent();
                foreach (var f in data.Functions)
                {
                    builder.AppendLine($"{f.Name}Internal = ({f.Name}Delegate)Marshal.GetDelegateForFunctionPointer(Params.{f.Name}, typeof({f.Name}Delegate));");
                }
                builder.DecrementIndent();
                builder.AppendLine("}");
                builder.AppendLine("");

                foreach (var f in data.Functions)
                {
                    var attributes = f.GetAttributes();
                    var attribute = attributes.
                    Find(
                        (attribute) => {
                            var type = attribute.AttributeClass?.ToString();
                            return type == AttributeName;
                            }
                        );

                    var args = ((AttributeData)attribute).NamedArguments;
                    var generateAttribute = args.Find((pair) => pair.Key == "Generate");
                    if(generateAttribute.IsSome && (bool)generateAttribute.Value().Value.Value! == false)
                    {
                        continue;
                    }

                    string accessibility = "";
                    switch(f.DeclaredAccessibility)
                    {
                        case Accessibility.Private:
                            accessibility = "private"; break;
                        case Accessibility.Protected:
                            accessibility = "protected"; break;
                        case Accessibility.Internal:
                            accessibility = "internal"; break;
                        default:
                            accessibility = "public"; break;
                    }
                    var func = $"{accessibility} partial {f.ReturnType.OriginalDefinition} {f.Name}(";
                    var funcCall = "";
                    if (!f.ReturnsVoid)
                        funcCall = "return ";

                    funcCall += $"{f.Name}Internal((IntPtr)Address";

                    bool first = true;
                    foreach (var p in f.Parameters)
                    {
                        if (!first)
                            func += ", ";

                        first = false;
                        func += p.Type.OriginalDefinition + " " + p.Name;
                        funcCall += ", " + p.Name;
                    }
                    func = func + ")";
                    funcCall = funcCall + ");";

                    builder.AppendLine(func);
                    builder.AppendLine("{");
                    builder.IncrementIndent();

                    builder.AppendLine(funcCall);

                    builder.DecrementIndent();
                    builder.AppendLine("}");
                    builder.AppendLine("");
                }

                builder.DecrementIndent();
                builder.AppendLine("}");
                builder.DecrementIndent();
                builder.AppendLine("}");

                var s = builder.ToString();

                sourceContext.AddSource($"FunctionMemberGenerator.{ns}.{klassName}.g.cs", s);
            });
        }
    }
}