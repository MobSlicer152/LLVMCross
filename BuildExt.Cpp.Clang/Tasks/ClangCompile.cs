using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Build.CPPTasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace BuildExt.Cpp.Clang.Tasks
{
    // Token: 0x02000013 RID: 19
    public class ClangCompile : ClangToolTask
    {
        // Token: 0x060000E2 RID: 226 RVA: 0x000059EC File Offset: 0x00003BEC
        public ClangCompile()
        {
            this.switchOrderList = new ArrayList();
            this.switchOrderList.Add("AlwaysAppend");
            this.switchOrderList.Add("MSVCErrorReport");
            this.switchOrderList.Add("GccToolChain");
            this.switchOrderList.Add("TargetArch");
            this.switchOrderList.Add("Sysroot");
            this.switchOrderList.Add("ISystem");
            this.switchOrderList.Add("AdditionalIncludeDirectories");
            this.switchOrderList.Add("DebugInformationFormat");
            this.switchOrderList.Add("ObjectFileName");
            this.switchOrderList.Add("WarningLevel");
            this.switchOrderList.Add("TreatWarningAsError");
            this.switchOrderList.Add("Verbose");
            this.switchOrderList.Add("TrackerLogDirectory");
            this.switchOrderList.Add("Optimization");
            this.switchOrderList.Add("StrictAliasing");
            this.switchOrderList.Add("ThumbMode");
            this.switchOrderList.Add("OmitFramePointers");
            this.switchOrderList.Add("ExceptionHandling");
            this.switchOrderList.Add("FunctionLevelLinking");
            this.switchOrderList.Add("DataLevelLinking");
            this.switchOrderList.Add("EnableNeonCodegen");
            this.switchOrderList.Add("FloatABI");
            this.switchOrderList.Add("BufferSecurityCheck");
            this.switchOrderList.Add("PositionIndependentCode");
            this.switchOrderList.Add("UseShortEnums");
            this.switchOrderList.Add("RuntimeLibrary");
            this.switchOrderList.Add("RuntimeTypeInfo");
            this.switchOrderList.Add("CLanguageStandard");
            this.switchOrderList.Add("CppLanguageStandard");
            this.switchOrderList.Add("PreprocessorDefinitions");
            this.switchOrderList.Add("UndefinePreprocessorDefinitions");
            this.switchOrderList.Add("UndefineAllPreprocessorDefinitions");
            this.switchOrderList.Add("Freestanding");
            this.switchOrderList.Add("ShowIncludes");
            this.switchOrderList.Add("PrecompiledHeader");
            this.switchOrderList.Add("PrecompiledHeaderFile");
            this.switchOrderList.Add("PrecompiledHeaderOutputFileDirectory");
            this.switchOrderList.Add("PrecompiledHeaderCompileAs");
            this.switchOrderList.Add("CompileAs");
            this.switchOrderList.Add("ForcedIncludeFiles");
            this.switchOrderList.Add("UseMultiToolTask");
            this.switchOrderList.Add("MSCompatibility");
            this.switchOrderList.Add("MSCompatibilityVersion");
            this.switchOrderList.Add("MSExtensions");
            this.switchOrderList.Add("MSCompilerVersion");
            this.switchOrderList.Add("AdditionalOptions");
            this.switchOrderList.Add("Sources");
            this.switchOrderList.Add("BuildingInIde");
        }

        // Token: 0x1700002B RID: 43
        // (get) Token: 0x060000E3 RID: 227 RVA: 0x00005D82 File Offset: 0x00003F82
        protected override string ToolName
        {
            get
            {
                return "clang.exe";
            }
        }

        // Token: 0x1700002C RID: 44
        // (get) Token: 0x060000E4 RID: 228 RVA: 0x00005D89 File Offset: 0x00003F89
        protected override string AlwaysAppend
        {
            get
            {
                return "-c";
            }
        }

        // Token: 0x1700002D RID: 45
        // (get) Token: 0x060000E5 RID: 229 RVA: 0x00005D90 File Offset: 0x00003F90
        // (set) Token: 0x060000E6 RID: 230 RVA: 0x00005DB8 File Offset: 0x00003FB8
        public virtual bool MSVCErrorReport
        {
            get
            {
                return base.IsPropertySet("MSVCErrorReport") && base.ActiveToolSwitches["MSVCErrorReport"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("MSVCErrorReport");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Visual Studio Errors Reporting";
                toolSwitch.Description = "Report errors which Visual Studio can use to parse for file and line information.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fdiagnostics-format=msvc";
                toolSwitch.Name = "MSVCErrorReport";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("MSVCErrorReport", toolSwitch);
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
                toolSwitch.ArgumentRequired = true;
                toolSwitch.SwitchValue = "--target=";
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

        // Token: 0x17000031 RID: 49
        // (get) Token: 0x060000ED RID: 237 RVA: 0x0000601F File Offset: 0x0000421F
        // (set) Token: 0x060000EE RID: 238 RVA: 0x00006048 File Offset: 0x00004248
        public virtual string[] ISystem
        {
            get
            {
                if (base.IsPropertySet("ISystem"))
                {
                    return base.ActiveToolSwitches["ISystem"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ISystem");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringPathArray);
                toolSwitch.DisplayName = "System include search path";
                toolSwitch.Description = "Folder path to the directory for SYSTEM include search path.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-isystem ";
                toolSwitch.Name = "ISystem";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("ISystem", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000032 RID: 50
        // (get) Token: 0x060000EF RID: 239 RVA: 0x000060C4 File Offset: 0x000042C4
        // (set) Token: 0x060000F0 RID: 240 RVA: 0x000060EC File Offset: 0x000042EC
        public virtual string[] AdditionalIncludeDirectories
        {
            get
            {
                if (base.IsPropertySet("AdditionalIncludeDirectories"))
                {
                    return base.ActiveToolSwitches["AdditionalIncludeDirectories"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("AdditionalIncludeDirectories");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringPathArray);
                toolSwitch.DisplayName = "Additional Include Directories";
                toolSwitch.Description = "Specifies one or more directories to add to the include path; separate with semi-colons if more than one. (-I[path]).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-I ";
                toolSwitch.Name = "AdditionalIncludeDirectories";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("AdditionalIncludeDirectories", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000033 RID: 51
        // (get) Token: 0x060000F1 RID: 241 RVA: 0x00006168 File Offset: 0x00004368
        // (set) Token: 0x060000F2 RID: 242 RVA: 0x00006190 File Offset: 0x00004390
        public virtual string DebugInformationFormat
        {
            get
            {
                if (base.IsPropertySet("DebugInformationFormat"))
                {
                    return base.ActiveToolSwitches["DebugInformationFormat"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("DebugInformationFormat");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Debug Information Format";
                toolSwitch.Description = "Specifies the type of debugging information generated by the compiler.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "None", "-g0" },
                    new string[] { "FullDebug", "-g2 -gdwarf-2" },
                    new string[] { "LineNumber", "-gline-tables-only" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("DebugInformationFormat", array, value);
                toolSwitch.Name = "DebugInformationFormat";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("DebugInformationFormat", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000034 RID: 52
        // (get) Token: 0x060000F3 RID: 243 RVA: 0x0000626C File Offset: 0x0000446C
        // (set) Token: 0x060000F4 RID: 244 RVA: 0x00006294 File Offset: 0x00004494
        public virtual string ObjectFileName
        {
            get
            {
                if (base.IsPropertySet("ObjectFileName"))
                {
                    return base.ActiveToolSwitches["ObjectFileName"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ObjectFileName");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.File);
                toolSwitch.DisplayName = "Object File Name";
                toolSwitch.Description = "Specifies a name to override the default object file name; can be file or directory name. (/Fo[name]).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-o ";
                toolSwitch.Name = "ObjectFileName";
                toolSwitch.Value = value;
                base.ActiveToolSwitches.Add("ObjectFileName", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000035 RID: 53
        // (get) Token: 0x060000F5 RID: 245 RVA: 0x0000630F File Offset: 0x0000450F
        // (set) Token: 0x060000F6 RID: 246 RVA: 0x00006338 File Offset: 0x00004538
        public virtual string WarningLevel
        {
            get
            {
                if (base.IsPropertySet("WarningLevel"))
                {
                    return base.ActiveToolSwitches["WarningLevel"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("WarningLevel");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Warning Level";
                toolSwitch.Description = "Select how strict you want the compiler to be about code errors.  Other flags should be added directly to Additional Options. (/w, /Weverything).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "TurnOffAllWarnings", "-w" },
                    new string[] { "EnableAllWarnings", "-Wall" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("WarningLevel", array, value);
                toolSwitch.Name = "WarningLevel";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("WarningLevel", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000036 RID: 54
        // (get) Token: 0x060000F7 RID: 247 RVA: 0x000063FB File Offset: 0x000045FB
        // (set) Token: 0x060000F8 RID: 248 RVA: 0x00006424 File Offset: 0x00004624
        public virtual bool TreatWarningAsError
        {
            get
            {
                return base.IsPropertySet("TreatWarningAsError") && base.ActiveToolSwitches["TreatWarningAsError"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("TreatWarningAsError");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Treat Warnings As Errors";
                toolSwitch.Description = "Treats all compiler warnings as errors. For a new project, it may be best to use /WX in all compilations; resolving all warnings will ensure the fewest possible hard-to-find code defects.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-Werror";
                toolSwitch.Name = "TreatWarningAsError";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("TreatWarningAsError", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000037 RID: 55
        // (get) Token: 0x060000F9 RID: 249 RVA: 0x0000649F File Offset: 0x0000469F
        // (set) Token: 0x060000FA RID: 250 RVA: 0x000064C8 File Offset: 0x000046C8
        public virtual bool Verbose
        {
            get
            {
                return base.IsPropertySet("Verbose") && base.ActiveToolSwitches["Verbose"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Verbose");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Verbose mode";
                toolSwitch.Description = "Show commands to run and use verbose output.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-v";
                toolSwitch.Name = "Verbose";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Verbose", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

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

        // Token: 0x17000039 RID: 57
        // (get) Token: 0x060000FD RID: 253 RVA: 0x000065D6 File Offset: 0x000047D6
        // (set) Token: 0x060000FE RID: 254 RVA: 0x000065FC File Offset: 0x000047FC
        public virtual string Optimization
        {
            get
            {
                if (base.IsPropertySet("Optimization"))
                {
                    return base.ActiveToolSwitches["Optimization"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Optimization");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Optimization";
                toolSwitch.Description = "Specifies the optimization level for the application.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Custom", "" },
                    new string[] { "Disabled", "-O0" },
                    new string[] { "MinSize", "-Os" },
                    new string[] { "MaxSpeed", "-O2" },
                    new string[] { "Full", "-O3" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("Optimization", array, value);
                toolSwitch.Name = "Optimization";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("Optimization", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003A RID: 58
        // (get) Token: 0x060000FF RID: 255 RVA: 0x0000670A File Offset: 0x0000490A
        // (set) Token: 0x06000100 RID: 256 RVA: 0x00006730 File Offset: 0x00004930
        public virtual bool StrictAliasing
        {
            get
            {
                return base.IsPropertySet("StrictAliasing") && base.ActiveToolSwitches["StrictAliasing"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("StrictAliasing");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Strict Aliasing";
                toolSwitch.Description = "Assume the strictest aliasing rules.  An object of one type will never be assumed to reside at the same address as an object of a different type.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fstrict-aliasing";
                toolSwitch.ReverseSwitchValue = "-fno-strict-aliasing";
                toolSwitch.Name = "StrictAliasing";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("StrictAliasing", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003B RID: 59
        // (get) Token: 0x06000101 RID: 257 RVA: 0x000067B6 File Offset: 0x000049B6
        // (set) Token: 0x06000102 RID: 258 RVA: 0x000067DC File Offset: 0x000049DC
        public virtual string ThumbMode
        {
            get
            {
                if (base.IsPropertySet("ThumbMode"))
                {
                    return base.ActiveToolSwitches["ThumbMode"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ThumbMode");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Thumb Mode";
                toolSwitch.Description = "Generate code that executes for thumb microarchitecture. This is applicable for arm architecture only.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Thumb", "-mthumb" },
                    new string[] { "ARM", "-marm" },
                    new string[] { "Disabled", "" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("ThumbMode", array, value);
                toolSwitch.Name = "ThumbMode";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("ThumbMode", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003C RID: 60
        // (get) Token: 0x06000103 RID: 259 RVA: 0x000068B8 File Offset: 0x00004AB8
        // (set) Token: 0x06000104 RID: 260 RVA: 0x000068E0 File Offset: 0x00004AE0
        public virtual bool OmitFramePointers
        {
            get
            {
                return base.IsPropertySet("OmitFramePointers") && base.ActiveToolSwitches["OmitFramePointers"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("OmitFramePointers");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Omit Frame Pointer";
                toolSwitch.Description = "Suppresses creation of frame pointers on the call stack.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fomit-frame-pointer";
                toolSwitch.ReverseSwitchValue = "-fno-omit-frame-pointer";
                toolSwitch.Name = "OmitFramePointers";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("OmitFramePointers", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003D RID: 61
        // (get) Token: 0x06000105 RID: 261 RVA: 0x00006966 File Offset: 0x00004B66
        // (set) Token: 0x06000106 RID: 262 RVA: 0x0000698C File Offset: 0x00004B8C
        public virtual string ExceptionHandling
        {
            get
            {
                if (base.IsPropertySet("ExceptionHandling"))
                {
                    return base.ActiveToolSwitches["ExceptionHandling"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ExceptionHandling");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Enable C++ Exceptions";
                toolSwitch.Description = "Specifies the model of exception handling to be used by the compiler.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Disabled", "-fno-exceptions" },
                    new string[] { "Enabled", "-fexceptions" },
                    new string[] { "UnwindTables", "-funwind-tables" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("ExceptionHandling", array, value);
                toolSwitch.Name = "ExceptionHandling";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("ExceptionHandling", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003E RID: 62
        // (get) Token: 0x06000107 RID: 263 RVA: 0x00006A68 File Offset: 0x00004C68
        // (set) Token: 0x06000108 RID: 264 RVA: 0x00006A90 File Offset: 0x00004C90
        public virtual bool FunctionLevelLinking
        {
            get
            {
                return base.IsPropertySet("FunctionLevelLinking") && base.ActiveToolSwitches["FunctionLevelLinking"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("FunctionLevelLinking");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Function-Level Linking";
                toolSwitch.Description = "Allows the compiler to package individual functions in the form of packaged functions (COMDATs). Required for edit and continue to work.     (ffunction-sections).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-ffunction-sections";
                toolSwitch.Name = "FunctionLevelLinking";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("FunctionLevelLinking", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700003F RID: 63
        // (get) Token: 0x06000109 RID: 265 RVA: 0x00006B0B File Offset: 0x00004D0B
        // (set) Token: 0x0600010A RID: 266 RVA: 0x00006B34 File Offset: 0x00004D34
        public virtual bool DataLevelLinking
        {
            get
            {
                return base.IsPropertySet("DataLevelLinking") && base.ActiveToolSwitches["DataLevelLinking"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("DataLevelLinking");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Data-Level Linking";
                toolSwitch.Description = "Enables linker optimizations to remove unused data by emitting each data item in a separate section.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fdata-sections";
                toolSwitch.Name = "DataLevelLinking";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("DataLevelLinking", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000040 RID: 64
        // (get) Token: 0x0600010B RID: 267 RVA: 0x00006BAF File Offset: 0x00004DAF
        // (set) Token: 0x0600010C RID: 268 RVA: 0x00006BD8 File Offset: 0x00004DD8
        public virtual bool EnableNeonCodegen
        {
            get
            {
                return base.IsPropertySet("EnableNeonCodegen") && base.ActiveToolSwitches["EnableNeonCodegen"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("EnableNeonCodegen");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Advanced SIMD(Neon)";
                toolSwitch.Description = "Enables code generation for NEON floating point hardware. This is applicable for arm architecture only.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-mfpu=neon";
                toolSwitch.Name = "EnableNeonCodegen";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("EnableNeonCodegen", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000041 RID: 65
        // (get) Token: 0x0600010D RID: 269 RVA: 0x00006C53 File Offset: 0x00004E53
        // (set) Token: 0x0600010E RID: 270 RVA: 0x00006C7C File Offset: 0x00004E7C
        public virtual string FloatABI
        {
            get
            {
                if (base.IsPropertySet("FloatABI"))
                {
                    return base.ActiveToolSwitches["FloatABI"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("FloatABI");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Floating-point ABI";
                toolSwitch.Description = "Selection option to choose the floating point ABI.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "soft", "-mfloat-abi=soft" },
                    new string[] { "softfp", "-mfloat-abi=softfp" },
                    new string[] { "hard", "-mfloat-abi=hard" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("FloatABI", array, value);
                toolSwitch.Name = "FloatABI";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("FloatABI", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000042 RID: 66
        // (get) Token: 0x0600010F RID: 271 RVA: 0x00006D58 File Offset: 0x00004F58
        // (set) Token: 0x06000110 RID: 272 RVA: 0x00006D80 File Offset: 0x00004F80
        public virtual string BufferSecurityCheck
        {
            get
            {
                if (base.IsPropertySet("BufferSecurityCheck"))
                {
                    return base.ActiveToolSwitches["BufferSecurityCheck"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("BufferSecurityCheck");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Security Check";
                toolSwitch.Description = "The Security Check helps detect stack-buffer over-runs, a common attempted attack upon a program's security. (fstack-protector).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "false", "" },
                    new string[] { "true", "-fstack-protector" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("BufferSecurityCheck", array, value);
                toolSwitch.Name = "BufferSecurityCheck";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("BufferSecurityCheck", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000043 RID: 67
        // (get) Token: 0x06000111 RID: 273 RVA: 0x00006E43 File Offset: 0x00005043
        // (set) Token: 0x06000112 RID: 274 RVA: 0x00006E6C File Offset: 0x0000506C
        public virtual bool PositionIndependentCode
        {
            get
            {
                return base.IsPropertySet("PositionIndependentCode") && base.ActiveToolSwitches["PositionIndependentCode"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PositionIndependentCode");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Position Independent Code";
                toolSwitch.Description = "Generate Position Independent Code (PIC) for use in a shared library.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fpic";
                toolSwitch.Name = "PositionIndependentCode";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("PositionIndependentCode", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000044 RID: 68
        // (get) Token: 0x06000113 RID: 275 RVA: 0x00006EE7 File Offset: 0x000050E7
        // (set) Token: 0x06000114 RID: 276 RVA: 0x00006F10 File Offset: 0x00005110
        public virtual bool UseShortEnums
        {
            get
            {
                return base.IsPropertySet("UseShortEnums") && base.ActiveToolSwitches["UseShortEnums"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UseShortEnums");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Use Short Enums";
                toolSwitch.Description = "Enum type uses only as many bytes required by input set of possible values.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fshort-enums";
                toolSwitch.ReverseSwitchValue = "-fno-short-enums";
                toolSwitch.Name = "UseShortEnums";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("UseShortEnums", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000046 RID: 70
        // (get) Token: 0x06000117 RID: 279 RVA: 0x000070B1 File Offset: 0x000052B1
        // (set) Token: 0x06000118 RID: 280 RVA: 0x000070D8 File Offset: 0x000052D8
        public virtual bool RuntimeTypeInfo
        {
            get
            {
                return base.IsPropertySet("RuntimeTypeInfo") && base.ActiveToolSwitches["RuntimeTypeInfo"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("RuntimeTypeInfo");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Enable Run-Time Type Information";
                toolSwitch.Description = "Adds code for checking C++ object types at run time (runtime type information).     (frtti, fno-rtti)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-frtti";
                toolSwitch.ReverseSwitchValue = "-fno-rtti";
                toolSwitch.Name = "RuntimeTypeInfo";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("RuntimeTypeInfo", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000047 RID: 71
        // (get) Token: 0x06000119 RID: 281 RVA: 0x0000715E File Offset: 0x0000535E
        // (set) Token: 0x0600011A RID: 282 RVA: 0x00007184 File Offset: 0x00005384
        public virtual string CLanguageStandard
        {
            get
            {
                if (base.IsPropertySet("CLanguageStandard"))
                {
                    return base.ActiveToolSwitches["CLanguageStandard"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("CLanguageStandard");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "C Language Standard";
                toolSwitch.Description = "Determines the C language standard.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Default", "" },
                    new string[] { "c89", "-std=c89" },
                    new string[] { "iso9899:199409", "-std=iso9899:199409" },
                    new string[] { "c99", "-std=c99" },
                    new string[] { "c11", "-std=c11" },
                    new string[] { "c17", "-std=c17" },
                    new string[] { "c23", "-std=c23" },
                    new string[] { "gnu89", "-std=gnu89" },
                    new string[] { "gnu99", "-std=gnu99" },
                    new string[] { "gnu11", "-std=gnu11" },
                    new string[] { "gnu17", "-std=gnu17" },
                    new string[] { "gnu23", "-std=gnu23" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("CLanguageStandard", array, value);
                toolSwitch.Name = "CLanguageStandard";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("CLanguageStandard", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000048 RID: 72
        // (get) Token: 0x0600011B RID: 283 RVA: 0x000072DD File Offset: 0x000054DD
        // (set) Token: 0x0600011C RID: 284 RVA: 0x00007304 File Offset: 0x00005504
        public virtual string CppLanguageStandard
        {
            get
            {
                if (base.IsPropertySet("CppLanguageStandard"))
                {
                    return base.ActiveToolSwitches["CppLanguageStandard"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("CppLanguageStandard");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "C++ Language Standard";
                toolSwitch.Description = "Determines the C++ language standard.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Default", "" },
                    new string[] { "c++98", "-std=c++98" },
                    new string[] { "c++11", "-std=c++11" },
                    new string[] { "c++14", "-std=c++14" },
                    new string[] { "c++17", "-std=c++17" },
                    new string[] { "c++20", "-std=c++20" },
                    new string[] { "c++23", "-std=c++23" },
                    new string[] { "c++26", "-std=c++26" },
                    new string[] { "gnu++98", "-std=gnu++98" },
                    new string[] { "gnu++11", "-std=gnu++11" },
                    new string[] { "gnu++14", "-std=gnu++14" },
                    new string[] { "gnu++17", "-std=gnu++17" },
                    new string[] { "gnu++20", "-std=gnu++20" },
                    new string[] { "gnu++23", "-std=gnu++23" },
                    new string[] { "gnu++26", "-std=gnu++26" },
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("CppLanguageStandard", array, value);
                toolSwitch.Name = "CppLanguageStandard";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("CppLanguageStandard", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000049 RID: 73
        // (get) Token: 0x0600011D RID: 285 RVA: 0x000074AB File Offset: 0x000056AB
        // (set) Token: 0x0600011E RID: 286 RVA: 0x000074D4 File Offset: 0x000056D4
        public virtual string[] PreprocessorDefinitions
        {
            get
            {
                if (base.IsPropertySet("PreprocessorDefinitions"))
                {
                    return base.ActiveToolSwitches["PreprocessorDefinitions"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PreprocessorDefinitions");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Preprocessor Definitions";
                toolSwitch.Description = "Defines a preprocessing symbols for your source file. (-D)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-D ";
                toolSwitch.Name = "PreprocessorDefinitions";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("PreprocessorDefinitions", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004A RID: 74
        // (get) Token: 0x0600011F RID: 287 RVA: 0x0000754F File Offset: 0x0000574F
        // (set) Token: 0x06000120 RID: 288 RVA: 0x00007578 File Offset: 0x00005778
        public virtual string[] UndefinePreprocessorDefinitions
        {
            get
            {
                if (base.IsPropertySet("UndefinePreprocessorDefinitions"))
                {
                    return base.ActiveToolSwitches["UndefinePreprocessorDefinitions"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UndefinePreprocessorDefinitions");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringArray);
                toolSwitch.DisplayName = "Undefine Preprocessor Definitions";
                toolSwitch.Description = "Specifies one or more preprocessor undefines.  (-U [macro])";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-U ";
                toolSwitch.Name = "UndefinePreprocessorDefinitions";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("UndefinePreprocessorDefinitions", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004B RID: 75
        // (get) Token: 0x06000121 RID: 289 RVA: 0x000075F3 File Offset: 0x000057F3
        // (set) Token: 0x06000122 RID: 290 RVA: 0x0000761C File Offset: 0x0000581C
        public virtual bool UndefineAllPreprocessorDefinitions
        {
            get
            {
                return base.IsPropertySet("UndefineAllPreprocessorDefinitions") && base.ActiveToolSwitches["UndefineAllPreprocessorDefinitions"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UndefineAllPreprocessorDefinitions");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Undefine All Preprocessor Definitions";
                toolSwitch.Description = "Undefine all previously defined preprocessor values.  (-undef)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-undef";
                toolSwitch.Name = "UndefineAllPreprocessorDefinitions";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("UndefineAllPreprocessorDefinitions", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        public virtual bool Freestanding
        {
            get
            {
                return base.IsPropertySet("Freestanding") && base.ActiveToolSwitches["Freestanding"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Freestanding");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Freestanding";
                toolSwitch.Description = "Freestanding mode.  (-ffreestanding)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-ffreestanding";
                toolSwitch.Name = "Freestanding";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Freestanding", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004C RID: 76
        // (get) Token: 0x06000123 RID: 291 RVA: 0x00007697 File Offset: 0x00005897
        // (set) Token: 0x06000124 RID: 292 RVA: 0x000076C0 File Offset: 0x000058C0
        public virtual bool ShowIncludes
        {
            get
            {
                return base.IsPropertySet("ShowIncludes") && base.ActiveToolSwitches["ShowIncludes"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ShowIncludes");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Show Includes";
                toolSwitch.Description = "Generates a list of include files with compiler output.  (-H)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-H";
                toolSwitch.Name = "ShowIncludes";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("ShowIncludes", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004D RID: 77
        // (get) Token: 0x06000125 RID: 293 RVA: 0x0000773B File Offset: 0x0000593B
        // (set) Token: 0x06000126 RID: 294 RVA: 0x00007764 File Offset: 0x00005964
        public virtual string PrecompiledHeader
        {
            get
            {
                if (base.IsPropertySet("PrecompiledHeader"))
                {
                    return base.ActiveToolSwitches["PrecompiledHeader"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PrecompiledHeader");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Precompiled Header";
                toolSwitch.Description = "Create/Use Precompiled Header:Enables creation or use of a precompiled header during the build.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Create", "" },
                    new string[] { "Use", "" },
                    new string[] { "NotUsing", "" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("PrecompiledHeader", array, value);
                toolSwitch.Name = "PrecompiledHeader";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("PrecompiledHeader", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004E RID: 78
        // (get) Token: 0x06000127 RID: 295 RVA: 0x00007840 File Offset: 0x00005A40
        // (set) Token: 0x06000128 RID: 296 RVA: 0x00007868 File Offset: 0x00005A68
        public virtual string PrecompiledHeaderFile
        {
            get
            {
                if (base.IsPropertySet("PrecompiledHeaderFile"))
                {
                    return base.ActiveToolSwitches["PrecompiledHeaderFile"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PrecompiledHeaderFile");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.File);
                toolSwitch.DisplayName = "Precompiled Header File";
                toolSwitch.Description = "Specifies header file name to use for precompiled header file. This file will be also added to 'Forced Include Files' during build";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "PrecompiledHeaderFile";
                toolSwitch.Value = value;
                base.ActiveToolSwitches.Add("PrecompiledHeaderFile", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x1700004F RID: 79
        // (get) Token: 0x06000129 RID: 297 RVA: 0x000078D8 File Offset: 0x00005AD8
        // (set) Token: 0x0600012A RID: 298 RVA: 0x00007900 File Offset: 0x00005B00
        public virtual string PrecompiledHeaderOutputFileDirectory
        {
            get
            {
                if (base.IsPropertySet("PrecompiledHeaderOutputFileDirectory"))
                {
                    return base.ActiveToolSwitches["PrecompiledHeaderOutputFileDirectory"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PrecompiledHeaderOutputFileDirectory");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Precompiled Header Output File Directory";
                toolSwitch.Description = "Specifies the directory for the generated precompiled header. This directory will be also added to 'Additional Include Directories' during build";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "PrecompiledHeaderOutputFileDirectory";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "";
                base.ActiveToolSwitches.Add("PrecompiledHeaderOutputFileDirectory", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000050 RID: 80
        // (get) Token: 0x0600012B RID: 299 RVA: 0x0000797B File Offset: 0x00005B7B
        // (set) Token: 0x0600012C RID: 300 RVA: 0x000079A4 File Offset: 0x00005BA4
        public virtual string PrecompiledHeaderCompileAs
        {
            get
            {
                if (base.IsPropertySet("PrecompiledHeaderCompileAs"))
                {
                    return base.ActiveToolSwitches["PrecompiledHeaderCompileAs"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("PrecompiledHeaderCompileAs");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Compile Precompiled Header As";
                toolSwitch.Description = "Select compile language option for precompiled header file (-x c-header, -x c++-header).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "CompileAsC", "-x c-header" },
                    new string[] { "CompileAsCpp", "-x c++-header" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("PrecompiledHeaderCompileAs", array, value);
                toolSwitch.Name = "PrecompiledHeaderCompileAs";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("PrecompiledHeaderCompileAs", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000051 RID: 81
        // (get) Token: 0x0600012D RID: 301 RVA: 0x00007A67 File Offset: 0x00005C67
        // (set) Token: 0x0600012E RID: 302 RVA: 0x00007A90 File Offset: 0x00005C90
        public virtual string CompileAs
        {
            get
            {
                if (base.IsPropertySet("CompileAs"))
                {
                    return base.ActiveToolSwitches["CompileAs"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("CompileAs");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Compile As";
                toolSwitch.Description = "Select compile language option for .c and .cpp files.  'Default' will detect based on .c or .cpp extention. (-x c, -x c++)";
                toolSwitch.ArgumentRelationList = new ArrayList();
                string[][] array = new string[][]
                {
                    new string[] { "Default", "" },
                    new string[] { "CompileAsC", "-x c" },
                    new string[] { "CompileAsCpp", "-x c++" },
                    new string[] { "CompileAsAsm", "-x assembler-with-cpp" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("CompileAs", array, value);
                toolSwitch.Name = "CompileAs";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("CompileAs", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000052 RID: 82
        // (get) Token: 0x0600012F RID: 303 RVA: 0x00007B6C File Offset: 0x00005D6C
        // (set) Token: 0x06000130 RID: 304 RVA: 0x00007B94 File Offset: 0x00005D94
        public virtual string[] ForcedIncludeFiles
        {
            get
            {
                if (base.IsPropertySet("ForcedIncludeFiles"))
                {
                    return base.ActiveToolSwitches["ForcedIncludeFiles"].StringList;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("ForcedIncludeFiles");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.StringPathArray);
                toolSwitch.DisplayName = "Forced Include Files";
                toolSwitch.Description = "one or more forced include files.     (-include [name])";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-include ";
                toolSwitch.Name = "ForcedIncludeFiles";
                toolSwitch.StringList = value;
                base.ActiveToolSwitches.Add("ForcedIncludeFiles", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000053 RID: 83
        // (get) Token: 0x06000131 RID: 305 RVA: 0x00007C10 File Offset: 0x00005E10
        // (set) Token: 0x06000132 RID: 306 RVA: 0x00007C38 File Offset: 0x00005E38
        public virtual bool UseMultiToolTask
        {
            get
            {
                return base.IsPropertySet("UseMultiToolTask") && base.ActiveToolSwitches["UseMultiToolTask"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("UseMultiToolTask");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Multi-processor Compilation";
                toolSwitch.Description = "Multi-processor Compilation.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "UseMultiToolTask";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("UseMultiToolTask", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000054 RID: 84
        // (get) Token: 0x06000133 RID: 307 RVA: 0x00007CA8 File Offset: 0x00005EA8
        // (set) Token: 0x06000134 RID: 308 RVA: 0x00007CD0 File Offset: 0x00005ED0
        public virtual bool MSCompatibility
        {
            get
            {
                return base.IsPropertySet("MSCompatibility") && base.ActiveToolSwitches["MSCompatibility"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("MSCompatibility");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Microsoft Compatibility Mode";
                toolSwitch.Description = "Enable full Microsoft Visual C++ compatibility.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fms-compatibility";
                toolSwitch.ReverseSwitchValue = "-fno-ms-compatibility";
                toolSwitch.Name = "MSCompatibility";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("MSCompatibility", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000055 RID: 85
        // (get) Token: 0x06000135 RID: 309 RVA: 0x00007D56 File Offset: 0x00005F56
        // (set) Token: 0x06000136 RID: 310 RVA: 0x00007D7C File Offset: 0x00005F7C
        public virtual string MSCompatibilityVersion
        {
            get
            {
                if (base.IsPropertySet("MSCompatibilityVersion"))
                {
                    return base.ActiveToolSwitches["MSCompatibilityVersion"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("MSCompatibilityVersion");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Microsoft Compatibility Mode Version";
                toolSwitch.Description = "Dot-separated value representing the Microsoft compiler version number to report in _MSC_VER (0 = don't define it (default)).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "MSCompatibilityVersion";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "-fms-compatibility-version=";
                base.ActiveToolSwitches.Add("MSCompatibilityVersion", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000056 RID: 86
        // (get) Token: 0x06000137 RID: 311 RVA: 0x00007DF7 File Offset: 0x00005FF7
        // (set) Token: 0x06000138 RID: 312 RVA: 0x00007E20 File Offset: 0x00006020
        public virtual bool MSExtensions
        {
            get
            {
                return base.IsPropertySet("MSExtensions") && base.ActiveToolSwitches["MSExtensions"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("MSExtensions");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Microsoft Extension Support";
                toolSwitch.Description = "Accept some non-standard constructs supported by the Microsoft compiler.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "-fms-extensions";
                toolSwitch.ReverseSwitchValue = "-fno-ms-extensions";
                toolSwitch.Name = "MSExtensions";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("MSExtensions", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000057 RID: 87
        // (get) Token: 0x06000139 RID: 313 RVA: 0x00007EA6 File Offset: 0x000060A6
        // (set) Token: 0x0600013A RID: 314 RVA: 0x00007ECC File Offset: 0x000060CC
        public virtual string MSCompilerVersion
        {
            get
            {
                if (base.IsPropertySet("MSCompilerVersion"))
                {
                    return base.ActiveToolSwitches["MSCompilerVersion"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("MSCompilerVersion");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Microsoft Compiler Version";
                toolSwitch.Description = "Microsoft compiler version number to report in _MSC_VER (0 = don't define it (default)).";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "MSCompilerVersion";
                toolSwitch.Value = value;
                toolSwitch.SwitchValue = "-fmsc-version=";
                base.ActiveToolSwitches.Add("MSCompilerVersion", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x17000058 RID: 88
        // (get) Token: 0x0600013B RID: 315 RVA: 0x00007F47 File Offset: 0x00006147
        // (set) Token: 0x0600013C RID: 316 RVA: 0x00007F70 File Offset: 0x00006170
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

        // Token: 0x17000059 RID: 89
        // (get) Token: 0x0600013D RID: 317 RVA: 0x00007FD1 File Offset: 0x000061D1
        // (set) Token: 0x0600013E RID: 318 RVA: 0x00007FF8 File Offset: 0x000061F8
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

        // Token: 0x1700005A RID: 90
        // (get) Token: 0x0600013F RID: 319 RVA: 0x00008052 File Offset: 0x00006252
        protected override ArrayList SwitchOrderList
        {
            get
            {
                return this.switchOrderList;
            }
        }

        // Token: 0x1700005B RID: 91
        // (get) Token: 0x06000140 RID: 320 RVA: 0x0000805A File Offset: 0x0000625A
        // (set) Token: 0x06000141 RID: 321 RVA: 0x00008062 File Offset: 0x00006262
        public bool GNUMode { get; set; }

        // Token: 0x1700005C RID: 92
        // (get) Token: 0x06000142 RID: 322 RVA: 0x0000806B File Offset: 0x0000626B
        // (set) Token: 0x06000143 RID: 323 RVA: 0x00008073 File Offset: 0x00006273
        public string ClangVersion { get; set; }

        // Token: 0x1700005D RID: 93
        // (get) Token: 0x06000144 RID: 324 RVA: 0x0000807C File Offset: 0x0000627C
        protected override bool MaintainCompositeRootingMarkers
        {
            get
            {
                return false;
            }
        }

        // Token: 0x1700005E RID: 94
        // (get) Token: 0x06000145 RID: 325 RVA: 0x00008080 File Offset: 0x00006280
        protected override string[] ReadTLogNames
        {
            get
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.ToolExe);
                return new string[]
                {
                    this.firstReadTlog,
                    fileNameWithoutExtension + ".read.*.tlog",
                    fileNameWithoutExtension + ".*.read.*.tlog",
                    fileNameWithoutExtension + "-*.read.*.tlog",
                    fileNameWithoutExtension + ".delete.*.tlog",
                    fileNameWithoutExtension + ".*.delete.*.tlog",
                    fileNameWithoutExtension + "-*.delete.*.tlog"
                };
            }
        }

        // Token: 0x1700005F RID: 95
        // (get) Token: 0x06000146 RID: 326 RVA: 0x000080FC File Offset: 0x000062FC
        protected override string[] WriteTLogNames
        {
            get
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.ToolExe);
                return new string[]
                {
                    this.firstWriteTlog,
                    fileNameWithoutExtension + ".write.*.tlog",
                    fileNameWithoutExtension + ".*.write.*.tlog",
                    fileNameWithoutExtension + "-*.write.*.tlog"
                };
            }
        }

        // Token: 0x17000060 RID: 96
        // (get) Token: 0x06000147 RID: 327 RVA: 0x00008150 File Offset: 0x00006350
        protected override string CommandTLogName
        {
            get
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.ToolExe);
                return fileNameWithoutExtension + ".command.1.tlog";
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

        // Token: 0x17000063 RID: 99
        // (get) Token: 0x0600014A RID: 330 RVA: 0x00008192 File Offset: 0x00006392
        protected override bool TrackReplaceFile
        {
            get
            {
                return true;
            }
        }

        // Token: 0x0600014B RID: 331 RVA: 0x00008198 File Offset: 0x00006398
        protected override void RemoveTaskSpecificInputs(CanonicalTrackedInputFiles compactInputs)
        {
            if (base.IsPropertySet("PrecompiledHeader") && this.PrecompiledHeader != "Create")
            {
                return;
            }
            if (base.IsPropertySet("ObjectFileName"))
            {
                string objectFileName = this.ObjectFileName;
                TaskItem taskItem = new TaskItem(objectFileName);
                compactInputs.RemoveDependencyFromEntry(this.Sources, taskItem);
                return;
            }
        }

        // Token: 0x0600014C RID: 332 RVA: 0x000081F0 File Offset: 0x000063F0
        protected override int ExecuteTool(string pathToTool, string responseFileCommands, string commandLineCommands)
        {
            foreach (ITaskItem taskItem in base.SourcesCompiled)
            {
                base.Log.LogMessage(MessageImportance.High, Path.GetFileName(taskItem.ItemSpec), Array.Empty<object>());
            }
            int num = 0;
            for (; ; )
            {
                num++;
                if (!File.Exists(Path.Combine(this.TrackerIntermediateDirectory, this.firstReadTlog)))
                {
                    try
                    {
                        using (File.Create(Path.Combine(this.TrackerIntermediateDirectory, this.firstReadTlog)))
                        {
                        }
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(50);
                        goto IL_00CF;
                    }
                    goto IL_0085;
                }
                goto IL_0085;
            IL_00CF:
                if (num >= 30)
                {
                    break;
                }
                continue;
            IL_0085:
                if (!File.Exists(Path.Combine(this.TrackerIntermediateDirectory, this.firstWriteTlog)))
                {
                    try
                    {
                        using (File.Create(Path.Combine(this.TrackerIntermediateDirectory, this.firstWriteTlog)))
                        {
                        }
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(50);
                    }
                    goto IL_00CF;
                }
                break;
            }
            if (this.GNUMode)
            {
                this.errorListRegexList.Add(ClangCompile.gccMessageRegex);
            }
            return base.ExecuteTool(pathToTool, responseFileCommands, commandLineCommands);
        }

        // Token: 0x17000064 RID: 100
        // (get) Token: 0x0600014D RID: 333 RVA: 0x0000832C File Offset: 0x0000652C
        protected override Encoding ResponseFileEncoding
        {
            get
            {
                if (!this.GNUMode)
                {
                    return base.ResponseFileEncoding;
                }
                return new UTF8Encoding(false);
            }
        }

        // Token: 0x0600014E RID: 334 RVA: 0x00008344 File Offset: 0x00006544
        protected override string GenerateResponseFileCommandsExceptSwitches(string[] switchesToRemove, VCToolTask.CommandLineFormat format = VCToolTask.CommandLineFormat.ForBuildLog, VCToolTask.EscapeFormat escapeFormat = VCToolTask.EscapeFormat.EscapeTrailingSlash)
        {
            string text = base.GenerateResponseFileCommandsExceptSwitches(switchesToRemove, format, VCToolTask.EscapeFormat.EscapeTrailingSlash);
            text = VCToolTask.FindBackSlashInPath.Replace(text, "\\\\");
            return text;
        }

        // Token: 0x17000065 RID: 101
        // (get) Token: 0x0600014F RID: 335 RVA: 0x00008370 File Offset: 0x00006570
        protected override Encoding StandardOutputEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        // Token: 0x17000066 RID: 102
        // (get) Token: 0x06000150 RID: 336 RVA: 0x00008377 File Offset: 0x00006577
        protected override Encoding StandardErrorEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        // Token: 0x04000046 RID: 70
        private ArrayList switchOrderList;

        // Token: 0x04000047 RID: 71
        private ITaskItem[] preprocessOutput = new ITaskItem[0];

        // Token: 0x04000048 RID: 72
        private string firstReadTlog = typeof(ClangCompile).FullName + ".read.1.tlog";

        // Token: 0x04000049 RID: 73
        private string firstWriteTlog = typeof(ClangCompile).FullName + ".write.1.tlog";

        // Token: 0x0400004A RID: 74
        private static Regex gccMessageRegex = new Regex("^\\s*(?<FILENAME>[^:]*):(?<LINE>\\d*):(?<COLUMN>\\d*)\\s*:\\s*(?<CATEGORY>fatal error|error|warning|note):(?<TEXT>.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
