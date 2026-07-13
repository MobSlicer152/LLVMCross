using Microsoft.Build.CPPTasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Shared;
using Microsoft.Build.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace BuildExt.Cpp.Clang.Tasks
{
    // Token: 0x02000044 RID: 68
    public class Ld : ClangToolTask
    {
        // Token: 0x0600037B RID: 891 RVA: 0x0000EAFC File Offset: 0x0000CCFC
        public Ld()
        {
            this.switchOrderList = new ArrayList();
            this.switchOrderList.Add("GccToolChain");
            this.switchOrderList.Add("TargetArch");
            this.switchOrderList.Add("Sysroot");
            this.switchOrderList.Add("OutputFile");
            this.switchOrderList.Add("ShowProgress");
            this.switchOrderList.Add("Version");
            this.switchOrderList.Add("VerboseOutput");
            this.switchOrderList.Add("Trace");
            this.switchOrderList.Add("TraceSymbols");
            this.switchOrderList.Add("PrintMap");
            this.switchOrderList.Add("UnresolvedSymbolReferences");
            this.switchOrderList.Add("OptimizeforMemory");
            this.switchOrderList.Add("SharedLibrarySearchPath");
            this.switchOrderList.Add("AdditionalLibraryDirectories");
            this.switchOrderList.Add("IgnoreSpecificDefaultLibraries");
            this.switchOrderList.Add("IgnoreDefaultLibraries");
            this.switchOrderList.Add("ForceUndefineSymbolReferences");
            this.switchOrderList.Add("DebuggerSymbolInformation");
            this.switchOrderList.Add("GenerateMapFile");
            this.switchOrderList.Add("Relocation");
            this.switchOrderList.Add("FunctionBinding");
            this.switchOrderList.Add("NoExecStackRequired");
            this.switchOrderList.Add("LinkDll");
            this.switchOrderList.Add("WholeArchiveBegin");
            this.switchOrderList.Add("AdditionalOptions");
            this.switchOrderList.Add("Sources");
            this.switchOrderList.Add("AdditionalDependencies");
            this.switchOrderList.Add("WholeArchiveEnd");
            this.switchOrderList.Add("LibraryDependencies");
            this.switchOrderList.Add("BuildingInIde");
            this.switchOrderList.Add("EnableASAN");
            this.switchOrderList.Add("UseOfStl");
            this.errorListRegexList.Add(Ld._fileLineTextExpression);
        }

        // Token: 0x170000ED RID: 237
        // (get) Token: 0x0600037C RID: 892 RVA: 0x0000ED1E File Offset: 0x0000CF1E
        protected override string ToolName
        {
            get
            {
                return "clang.exe";
            }
        }

        // Token: 0x170000EE RID: 238
        // (get) Token: 0x0600037D RID: 893 RVA: 0x0000ED25 File Offset: 0x0000CF25
        // (set) Token: 0x0600037E RID: 894 RVA: 0x0000ED4C File Offset: 0x0000CF4C
        public virtual string OutputFile
        {
            get
            {
                if (base.IsPropertySet("OutputFile"))
                {
                    return base.ActiveToolSwitches["OutputFile"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("OutputFile");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.File);
                toolSwitch.DisplayName = "Output File";
                toolSwitch.Description = "The option overrides the default name and location of the program that the linker creates. (-o)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-o ";
                toolSwitch.Name = "OutputFile";
                toolSwitch.Value = value;
                base.ActiveToolSwitches.Add("OutputFile", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700002E RID: 46
        // (get) Token: 0x060000E7 RID: 231 RVA: 0x00005E33 File Offset: 0x00004033
        // (set) Token: 0x060000E8 RID: 232 RVA: 0x00005E5C File Offset: 0x0000405C
        public virtual string GccToolChain
        {
            get
            {
                if (base.IsPropertySet("GccToolChain"))
                {
                    return base.ActiveToolSwitches["GccToolChain"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("GccToolChain");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Gcc Tool Chain";
                toolSwitch.Description = "Folder path to Gcc Tool Chain.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "GccToolChain";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "--gcc-toolchain=";
                base.ActiveToolSwitches.Add("GccToolChain", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700002F RID: 47
        // (get) Token: 0x060000E9 RID: 233 RVA: 0x00005ED7 File Offset: 0x000040D7
        // (set) Token: 0x060000EA RID: 234 RVA: 0x00005F00 File Offset: 0x00004100
        public virtual string TargetArch
        {
            get
            {
                if (base.IsPropertySet("TargetArch"))
                {
                    return base.ActiveToolSwitches["TargetArch"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("TargetArch");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Target Architecture";
                toolSwitch.Description = "Target Architecture";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "TargetArch";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "--target=";
                toolSwitch.Required = true;
                base.ActiveToolSwitches.Add("TargetArch", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000030 RID: 48
        // (get) Token: 0x060000EB RID: 235 RVA: 0x00005F7B File Offset: 0x0000417B
        // (set) Token: 0x060000EC RID: 236 RVA: 0x00005FA4 File Offset: 0x000041A4
        public virtual string Sysroot
        {
            get
            {
                if (base.IsPropertySet("Sysroot"))
                {
                    return base.ActiveToolSwitches["Sysroot"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Sysroot");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Sysroot";
                toolSwitch.Description = "Folder path to the root directory for headers and libraries.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "Sysroot";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "--sysroot=";
                base.ActiveToolSwitches.Add("Sysroot", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000EF RID: 239
        // (get) Token: 0x0600037F RID: 895 RVA: 0x0000EDC7 File Offset: 0x0000CFC7
        // (set) Token: 0x06000380 RID: 896 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
        public virtual bool ShowProgress
        {
            get
            {
                return base.IsPropertySet("ShowProgress") && base.ActiveToolSwitches["ShowProgress"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ShowProgress");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Show Progress";
                toolSwitch.Description = "Prints Linker Progress Messages.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--stats";
                toolSwitch.Name = "ShowProgress";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("ShowProgress", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F0 RID: 240
        // (get) Token: 0x06000381 RID: 897 RVA: 0x0000EE6B File Offset: 0x0000D06B
        // (set) Token: 0x06000382 RID: 898 RVA: 0x0000EE94 File Offset: 0x0000D094
        public virtual bool Version
        {
            get
            {
                return base.IsPropertySet("Version") && base.ActiveToolSwitches["Version"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Version");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Version";
                toolSwitch.Description = "The -version option tells the linker to put a version number in the header of the executable.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--version";
                toolSwitch.Name = "Version";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Version", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F1 RID: 241
        // (get) Token: 0x06000383 RID: 899 RVA: 0x0000EF0F File Offset: 0x0000D10F
        // (set) Token: 0x06000384 RID: 900 RVA: 0x0000EF38 File Offset: 0x0000D138
        public virtual bool VerboseOutput
        {
            get
            {
                return base.IsPropertySet("VerboseOutput") && base.ActiveToolSwitches["VerboseOutput"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("VerboseOutput");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Verbose Output";
                toolSwitch.Description = "The -verbose option tells the linker to output verbose messages for debugging.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "--verbose";
                toolSwitch.Name = "VerboseOutput";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("VerboseOutput", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F2 RID: 242
        // (get) Token: 0x06000385 RID: 901 RVA: 0x0000EFB3 File Offset: 0x0000D1B3
        // (set) Token: 0x06000386 RID: 902 RVA: 0x0000EFDC File Offset: 0x0000D1DC
        public virtual bool Trace
        {
            get
            {
                return base.IsPropertySet("Trace") && base.ActiveToolSwitches["Trace"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Trace");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Trace";
                toolSwitch.Description = "The --trace option tells the linker to output the input files as are processed.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--trace";
                toolSwitch.Name = "Trace";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Trace", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F3 RID: 243
        // (get) Token: 0x06000387 RID: 903 RVA: 0x0000F057 File Offset: 0x0000D257
        // (set) Token: 0x06000388 RID: 904 RVA: 0x0000F080 File Offset: 0x0000D280
        public virtual string[] TraceSymbols
        {
            get
            {
                if (base.IsPropertySet("TraceSymbols"))
                {
                    return base.ActiveToolSwitches["TraceSymbols"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("TraceSymbols");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Trace Symbols";
                toolSwitch.Description = "Print the list of files in which a symbol appears. (--trace-symbol=symbol)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--trace-symbol=";
                toolSwitch.Name = "TraceSymbols";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("TraceSymbols", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F4 RID: 244
        // (get) Token: 0x06000389 RID: 905 RVA: 0x0000F0FB File Offset: 0x0000D2FB
        // (set) Token: 0x0600038A RID: 906 RVA: 0x0000F124 File Offset: 0x0000D324
        public virtual bool PrintMap
        {
            get
            {
                return base.IsPropertySet("PrintMap") && base.ActiveToolSwitches["PrintMap"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PrintMap");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Print Map";
                toolSwitch.Description = "The --print-map option tells the linker to output a link map.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--print-map";
                toolSwitch.Name = "PrintMap";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("PrintMap", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F5 RID: 245
        // (get) Token: 0x0600038B RID: 907 RVA: 0x0000F19F File Offset: 0x0000D39F
        // (set) Token: 0x0600038C RID: 908 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
        public virtual bool UnresolvedSymbolReferences
        {
            get
            {
                return base.IsPropertySet("UnresolvedSymbolReferences") && base.ActiveToolSwitches["UnresolvedSymbolReferences"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UnresolvedSymbolReferences");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Report Unresolved Symbol References";
                toolSwitch.Description = "This option when enabled will report unresolved symbol references.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--no-undefined";
                toolSwitch.Name = "UnresolvedSymbolReferences";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("UnresolvedSymbolReferences", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F6 RID: 246
        // (get) Token: 0x0600038D RID: 909 RVA: 0x0000F243 File Offset: 0x0000D443
        // (set) Token: 0x0600038E RID: 910 RVA: 0x0000F26C File Offset: 0x0000D46C
        public virtual bool OptimizeforMemory
        {
            get
            {
                return base.IsPropertySet("OptimizeforMemory") && base.ActiveToolSwitches["OptimizeforMemory"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("OptimizeforMemory");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Optimize For Memory Usage";
                toolSwitch.Description = "Optimize for memory usage, by rereading the symbol tables as necessary.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--no-keep-memory";
                toolSwitch.Name = "OptimizeforMemory";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("OptimizeforMemory", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F7 RID: 247
        // (get) Token: 0x0600038F RID: 911 RVA: 0x0000F2E7 File Offset: 0x0000D4E7
        // (set) Token: 0x06000390 RID: 912 RVA: 0x0000F310 File Offset: 0x0000D510
        public virtual string[] SharedLibrarySearchPath
        {
            get
            {
                if (base.IsPropertySet("SharedLibrarySearchPath"))
                {
                    return base.ActiveToolSwitches["SharedLibrarySearchPath"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("SharedLibrarySearchPath");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringPathArray);
                toolSwitch.DisplayName = "Shared Library Search Path";
                toolSwitch.Description = "Allows the user to populate the shared library search path.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-rpath-link=";
                toolSwitch.Name = "SharedLibrarySearchPath";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("SharedLibrarySearchPath", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F8 RID: 248
        // (get) Token: 0x06000391 RID: 913 RVA: 0x0000F38C File Offset: 0x0000D58C
        // (set) Token: 0x06000392 RID: 914 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
        public virtual string[] AdditionalLibraryDirectories
        {
            get
            {
                if (base.IsPropertySet("AdditionalLibraryDirectories"))
                {
                    return base.ActiveToolSwitches["AdditionalLibraryDirectories"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("AdditionalLibraryDirectories");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringPathArray);
                toolSwitch.DisplayName = "Additional Library Directories";
                toolSwitch.Description = "Allows the user to override the environmental library path. (-L folder).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-L";
                toolSwitch.Name = "AdditionalLibraryDirectories";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("AdditionalLibraryDirectories", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000F9 RID: 249
        // (get) Token: 0x06000393 RID: 915 RVA: 0x0000F430 File Offset: 0x0000D630
        // (set) Token: 0x06000394 RID: 916 RVA: 0x0000F458 File Offset: 0x0000D658
        public virtual string[] IgnoreSpecificDefaultLibraries
        {
            get
            {
                if (base.IsPropertySet("IgnoreSpecificDefaultLibraries"))
                {
                    return base.ActiveToolSwitches["IgnoreSpecificDefaultLibraries"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("IgnoreSpecificDefaultLibraries");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Ignore Specific Default Libraries";
                toolSwitch.Description = "Specifies one or more names of default libraries to ignore.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--exclude-libs=";
                toolSwitch.Name = "IgnoreSpecificDefaultLibraries";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("IgnoreSpecificDefaultLibraries", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FA RID: 250
        // (get) Token: 0x06000395 RID: 917 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
        // (set) Token: 0x06000396 RID: 918 RVA: 0x0000F4FC File Offset: 0x0000D6FC
        public virtual bool IgnoreDefaultLibraries
        {
            get
            {
                return base.IsPropertySet("IgnoreDefaultLibraries") && base.ActiveToolSwitches["IgnoreDefaultLibraries"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("IgnoreDefaultLibraries");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Ignore Default Libraries";
                toolSwitch.Description = "Ignore default libraries and only search libraries explicitely specified.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-nostdlib";
                toolSwitch.Name = "IgnoreDefaultLibraries";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("IgnoreDefaultLibraries", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FB RID: 251
        // (get) Token: 0x06000397 RID: 919 RVA: 0x0000F577 File Offset: 0x0000D777
        // (set) Token: 0x06000398 RID: 920 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
        public virtual string[] ForceUndefineSymbolReferences
        {
            get
            {
                if (base.IsPropertySet("ForceUndefineSymbolReferences"))
                {
                    return base.ActiveToolSwitches["ForceUndefineSymbolReferences"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ForceUndefineSymbolReferences");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Force Symbol References";
                toolSwitch.Description = "Force symbol to be entered in the output file as an undefined symbol.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-u--undefined=";
                toolSwitch.Name = "ForceUndefineSymbolReferences";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("ForceUndefineSymbolReferences", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FC RID: 252
        // (get) Token: 0x06000399 RID: 921 RVA: 0x0000F61B File Offset: 0x0000D81B
        // (set) Token: 0x0600039A RID: 922 RVA: 0x0000F644 File Offset: 0x0000D844
        public virtual string DebuggerSymbolInformation
        {
            get
            {
                if (base.IsPropertySet("DebuggerSymbolInformation"))
                {
                    return base.ActiveToolSwitches["DebuggerSymbolInformation"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("DebuggerSymbolInformation");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Debugger Symbol Information";
                toolSwitch.Description = "Debugger symbol information from the output file.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "true", "" },
                    new string[] { "false", "" },
                    new string[] { "IncludeAll", "" },
                    new string[] { "OmitDebuggerSymbolInformation", "-Wl,--strip-debug" },
                    new string[] { "OmitAllSymbolInformation", "-Wl,--strip-all" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("DebuggerSymbolInformation", array, value);
                toolSwitch.Name = "DebuggerSymbolInformation";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("DebuggerSymbolInformation", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FD RID: 253
        // (get) Token: 0x0600039B RID: 923 RVA: 0x0000F752 File Offset: 0x0000D952
        // (set) Token: 0x0600039C RID: 924 RVA: 0x0000F778 File Offset: 0x0000D978
        public virtual string GenerateMapFile
        {
            get
            {
                if (base.IsPropertySet("GenerateMapFile"))
                {
                    return base.ActiveToolSwitches["GenerateMapFile"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("GenerateMapFile");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Map File Name";
                toolSwitch.Description = "The Map option tells the linker to create a map file with the user specified name.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "GenerateMapFile";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "-Wl,-Map=";
                base.ActiveToolSwitches.Add("GenerateMapFile", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FE RID: 254
        // (get) Token: 0x0600039D RID: 925 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
        // (set) Token: 0x0600039E RID: 926 RVA: 0x0000F81C File Offset: 0x0000DA1C
        public virtual bool Relocation
        {
            get
            {
                return base.IsPropertySet("Relocation") && base.ActiveToolSwitches["Relocation"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Relocation");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Mark Variables ReadOnly After Relocation";
                toolSwitch.Description = "This option marks variables read-only after relocation.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-z,relro";
                toolSwitch.ReverseSwitchValue = "-Wl,-z,norelro";
                toolSwitch.Name = "Relocation";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Relocation", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000FF RID: 255
        // (get) Token: 0x0600039F RID: 927 RVA: 0x0000F8A2 File Offset: 0x0000DAA2
        // (set) Token: 0x060003A0 RID: 928 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
        public virtual bool FunctionBinding
        {
            get
            {
                return base.IsPropertySet("FunctionBinding") && base.ActiveToolSwitches["FunctionBinding"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("FunctionBinding");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Immediate Function Binding";
                toolSwitch.Description = "This option marks object for immediate function binding.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-z,now";
                toolSwitch.Name = "FunctionBinding";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("FunctionBinding", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000100 RID: 256
        // (get) Token: 0x060003A1 RID: 929 RVA: 0x0000F943 File Offset: 0x0000DB43
        // (set) Token: 0x060003A2 RID: 930 RVA: 0x0000F96C File Offset: 0x0000DB6C
        public virtual bool NoExecStackRequired
        {
            get
            {
                return base.IsPropertySet("NoExecStackRequired") && base.ActiveToolSwitches["NoExecStackRequired"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("NoExecStackRequired");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Executable Stack Not Required";
                toolSwitch.Description = "This option marks output as not requiring executable stack.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,-z,noexecstack";
                toolSwitch.Name = "NoExecStackRequired";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("NoExecStackRequired", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000101 RID: 257
        // (get) Token: 0x060003A3 RID: 931 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
        // (set) Token: 0x060003A4 RID: 932 RVA: 0x0000FA10 File Offset: 0x0000DC10
        public virtual bool LinkDll
        {
            get
            {
                return base.IsPropertySet("LinkDll") && base.ActiveToolSwitches["LinkDll"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("LinkDll");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-shared";
                toolSwitch.Name = "LinkDll";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("LinkDll", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000102 RID: 258
        // (get) Token: 0x060003A5 RID: 933 RVA: 0x0000FA75 File Offset: 0x0000DC75
        // (set) Token: 0x060003A6 RID: 934 RVA: 0x0000FA9C File Offset: 0x0000DC9C
        public virtual bool WholeArchiveBegin
        {
            get
            {
                return base.IsPropertySet("WholeArchiveBegin") && base.ActiveToolSwitches["WholeArchiveBegin"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("WholeArchiveBegin");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Whole Archive";
                toolSwitch.Description = "Whole Archive uses all code from Sources and Additional Dependencies.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--whole-archive";
                toolSwitch.Name = "WholeArchiveBegin";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("WholeArchiveBegin", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000103 RID: 259
        // (get) Token: 0x060003A7 RID: 935 RVA: 0x0000FB17 File Offset: 0x0000DD17
        // (set) Token: 0x060003A8 RID: 936 RVA: 0x0000FB40 File Offset: 0x0000DD40
        [Required]
        public virtual ITaskItem[] Sources
        {
            get
            {
                if (base.IsPropertySet("Sources"))
                {
                    return base.ActiveToolSwitches["Sources"].TaskItemArray;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Sources");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.ITaskItemArray);
                toolSwitch.Separator = " ";
                toolSwitch.Required = true;
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.TaskItemArray = value;
                base.ActiveToolSwitches.Add("Sources", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000104 RID: 260
        // (get) Token: 0x060003A9 RID: 937 RVA: 0x0000FBA1 File Offset: 0x0000DDA1
        // (set) Token: 0x060003AA RID: 938 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
        public virtual string[] AdditionalDependencies
        {
            get
            {
                if (base.IsPropertySet("AdditionalDependencies"))
                {
                    return base.ActiveToolSwitches["AdditionalDependencies"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("AdditionalDependencies");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Additional Dependencies";
                toolSwitch.Description = "Specifies additional items to add to the link command line.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "AdditionalDependencies";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("AdditionalDependencies", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000105 RID: 261
        // (get) Token: 0x060003AB RID: 939 RVA: 0x0000FC38 File Offset: 0x0000DE38
        // (set) Token: 0x060003AC RID: 940 RVA: 0x0000FC60 File Offset: 0x0000DE60
        public virtual bool WholeArchiveEnd
        {
            get
            {
                return base.IsPropertySet("WholeArchiveEnd") && base.ActiveToolSwitches["WholeArchiveEnd"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("WholeArchiveEnd");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Wl,--no-whole-archive";
                toolSwitch.Name = "WholeArchiveEnd";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("WholeArchiveEnd", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000106 RID: 262
        // (get) Token: 0x060003AD RID: 941 RVA: 0x0000FCC5 File Offset: 0x0000DEC5
        // (set) Token: 0x060003AE RID: 942 RVA: 0x0000FCEC File Offset: 0x0000DEEC
        public virtual string[] LibraryDependencies
        {
            get
            {
                if (base.IsPropertySet("LibraryDependencies"))
                {
                    return base.ActiveToolSwitches["LibraryDependencies"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("LibraryDependencies");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Library Dependencies";
                toolSwitch.Description = "This option allows specifying additional libraries to be  added to the linker command line. The additional library will be added to the end of the linker command line  prefixed with 'lib' and end with the '.a' extension.  (-lNAME)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-l";
                toolSwitch.Name = "LibraryDependencies";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("LibraryDependencies", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000107 RID: 263
        // (get) Token: 0x060003AF RID: 943 RVA: 0x0000FD67 File Offset: 0x0000DF67
        // (set) Token: 0x060003B0 RID: 944 RVA: 0x0000FD90 File Offset: 0x0000DF90
        public virtual bool BuildingInIde
        {
            get
            {
                return base.IsPropertySet("BuildingInIde") && base.ActiveToolSwitches["BuildingInIde"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("BuildingInIde");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "BuildingInIde";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("BuildingInIde", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000108 RID: 264
        // (get) Token: 0x060003B1 RID: 945 RVA: 0x0000FDEA File Offset: 0x0000DFEA
        // (set) Token: 0x060003B2 RID: 946 RVA: 0x0000FE10 File Offset: 0x0000E010
        public virtual bool EnableASAN
        {
            get
            {
                return base.IsPropertySet("EnableASAN") && base.ActiveToolSwitches["EnableASAN"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("EnableASAN");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Address Sanitizer";
                toolSwitch.Description = "Link program with AddressSanitizer. Must also compiler with AddressSanitizer enabled. Must run with debugger to view diagnostic results.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fsanitize=address";
                toolSwitch.Name = "EnableASAN";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("EnableASAN", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000109 RID: 265
        // (get) Token: 0x060003B3 RID: 947 RVA: 0x0000FE8B File Offset: 0x0000E08B
        // (set) Token: 0x060003B4 RID: 948 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
        public virtual string UseOfStl
        {
            get
            {
                if (base.IsPropertySet("UseOfStl"))
                {
                    return base.ActiveToolSwitches["UseOfStl"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UseOfStl");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "c++_shared", "" },
                    new string[] { "c++_static", "-static-libc++" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("UseOfStl", array, value);
                toolSwitch.Name = "UseOfStl";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("UseOfStl", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700010A RID: 266
        // (get) Token: 0x060003B5 RID: 949 RVA: 0x0000FF61 File Offset: 0x0000E161
        protected override ArrayList SwitchOrderList
        {
            get
            {
                return this.switchOrderList;
            }
        }

        // Token: 0x1700010B RID: 267
        // (get) Token: 0x060003B6 RID: 950 RVA: 0x0000FF69 File Offset: 0x0000E169
        // (set) Token: 0x060003B7 RID: 951 RVA: 0x0000FF71 File Offset: 0x0000E171
        [Required]
        public string ProjectFileName { get; set; }

        // Token: 0x1700010D RID: 269
        // (get) Token: 0x060003BA RID: 954 RVA: 0x0000FF8B File Offset: 0x0000E18B
        // (set) Token: 0x060003BB RID: 955 RVA: 0x0000FF93 File Offset: 0x0000E193
        public bool IsCompileUpToDate { get; set; }

        // Token: 0x1700010F RID: 271
        // (get) Token: 0x060003BE RID: 958 RVA: 0x0000FFAD File Offset: 0x0000E1AD
        // (set) Token: 0x060003BF RID: 959 RVA: 0x0000FFB5 File Offset: 0x0000E1B5
        public bool CopyOutputFile { get; set; } = true;

        // Token: 0x17000038 RID: 56
        // (get) Token: 0x060000FB RID: 251 RVA: 0x00006543 File Offset: 0x00004743
        // (set) Token: 0x060000FC RID: 252 RVA: 0x0000656C File Offset: 0x0000476C
        public virtual string TrackerLogDirectory
        {
            get
            {
                if (base.IsPropertySet("TrackerLogDirectory"))
                {
                    return base.ActiveToolSwitches["TrackerLogDirectory"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("TrackerLogDirectory");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Directory);
                toolSwitch.DisplayName = "Tracker Log Directory";
                toolSwitch.Description = "Tracker Log Directory.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Value = VCToolTask.EnsureTrailingSlash(value);
                base.ActiveToolSwitches.Add("TrackerLogDirectory", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000061 RID: 97
        // (get) Token: 0x06000148 RID: 328 RVA: 0x00008174 File Offset: 0x00006374
        protected override string TrackerIntermediateDirectory
        {
            get
            {
                if (this.TrackerLogDirectory != null)
                {
                    return this.TrackerLogDirectory;
                }
                return string.Empty;
            }
        }

        // Token: 0x17000062 RID: 98
        // (get) Token: 0x06000149 RID: 329 RVA: 0x0000818A File Offset: 0x0000638A
        protected override ITaskItem[] TrackedInputFiles
        {
            get
            {
                return this.Sources;
            }
        }

        protected override string GenerateResponseFileCommandsExceptSwitches(string[] switchesToRemove, VCToolTask.CommandLineFormat format = VCToolTask.CommandLineFormat.ForBuildLog, VCToolTask.EscapeFormat escapeFormat = VCToolTask.EscapeFormat.EscapeTrailingSlash)
        {
            string text = base.GenerateResponseFileCommandsExceptSwitches(switchesToRemove, format, VCToolTask.EscapeFormat.EscapeTrailingSlash);
            text = VCToolTask.FindBackSlashInPath.Replace(text, "\\\\");
            return text;
        }

        // Token: 0x060003C0 RID: 960 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
        protected override void GenerateCommandsAccordingToType(CommandLineBuilder builder, ToolSwitch toolSwitch, VCToolTask.CommandLineFormat format = VCToolTask.CommandLineFormat.ForBuildLog, VCToolTask.EscapeFormat escapeFormat = VCToolTask.EscapeFormat.Default)
        {
            if (toolSwitch.Name.Equals("AdditionalDependencies", StringComparison.OrdinalIgnoreCase))
            {
                foreach (string text in toolSwitch.StringList)
                {
                    builder.AppendSwitchUnquotedIfNotNull("", text);
                }
                return;
            }
            base.GenerateCommandsAccordingToType(builder, toolSwitch, format, escapeFormat);
        }

        // Token: 0x060003C4 RID: 964 RVA: 0x0001034C File Offset: 0x0000E54C
        //protected override void LogEventsFromExecuteCommand(ICommand command, string log)
        //{
        //	StringReader stringReader = new StringReader(log);
        //	string text;
        //	while ((text = stringReader.ReadLine()) != null)
        //	{
        //		this.PrintMessage(this.ParseLine(text), MessageImportance.High);
        //	}
        //	this.PrintMessage(this.ParseLine(null), MessageImportance.High);
        //}

        // Token: 0x060003C5 RID: 965 RVA: 0x00010390 File Offset: 0x0000E590
        protected override void PrintMessage(VCToolTask.MessageStruct messageStruct, MessageImportance messageImportance)
        {
            if (messageStruct != null)
            {
                if (messageStruct.Category == "")
                {
                    messageStruct.Category = "error";
                }
                if (string.Compare(messageStruct.Category, "error", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(messageStruct.Category, "fatal error", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this._errorCount++;
                }
            }
            base.PrintMessage(messageStruct, messageImportance);
        }

        // Token: 0x04000151 RID: 337
        private const string FileLineTextPattern = "^\\s*(?<FILENAME>[^:]*):(((?<LINE>\\d*):)?)(\\s*(?<CATEGORY>(fatal error|error|warning|note)):)?\\s*(?<TEXT>.*)$";

        // Token: 0x04000152 RID: 338
        private static Regex _fileLineTextExpression = new Regex("^\\s*(?<FILENAME>[^:]*):(((?<LINE>\\d*):)?)(\\s*(?<CATEGORY>(fatal error|error|warning|note)):)?\\s*(?<TEXT>.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromMilliseconds(100.0));

        // Token: 0x04000153 RID: 339
        private int _errorCount;

        // Token: 0x04000154 RID: 340
        private ArrayList switchOrderList;
    }
}
