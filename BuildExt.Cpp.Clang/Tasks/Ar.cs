using Microsoft.Build.CPPTasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace BuildExt.Cpp.Clang.Tasks
{
    // Token: 0x02000042 RID: 66
    public class Ar : ClangToolTask
    {
        // Token: 0x06000342 RID: 834 RVA: 0x0000D78C File Offset: 0x0000B98C
        public Ar()
        {
            this.switchOrderList = new ArrayList();
            this.switchOrderList.Add("Command");
            this.switchOrderList.Add("CreateIndex");
            this.switchOrderList.Add("CreateThinArchive");
            this.switchOrderList.Add("NoWarnOnCreate");
            this.switchOrderList.Add("TruncateTimestamp");
            this.switchOrderList.Add("SuppressStartupBanner");
            this.switchOrderList.Add("Verbose");
            this.switchOrderList.Add("AdditionalOptions");
            this.switchOrderList.Add("OutputFile");
            this.switchOrderList.Add("Sources");
        }

        // Token: 0x170000D5 RID: 213
        // (get) Token: 0x06000343 RID: 835 RVA: 0x0000D85B File Offset: 0x0000BA5B
        protected override string ToolName
        {
            get
            {
                return "llvm-ar.exe";
            }
        }

        // Token: 0x170000D6 RID: 214
        // (get) Token: 0x06000344 RID: 836 RVA: 0x0000D862 File Offset: 0x0000BA62
        // (set) Token: 0x06000345 RID: 837 RVA: 0x0000D888 File Offset: 0x0000BA88
        public virtual string Command
        {
            get
            {
                if (base.IsPropertySet("Command"))
                {
                    return base.ActiveToolSwitches["Command"].Value;
                }
                return null;
            }
            set
            {
                base.ActiveToolSwitches.Remove("Command");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.String);
                toolSwitch.DisplayName = "Command";
                toolSwitch.Description = "Command for AR.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("CreateIndex", "", false, ""));
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("CreateThinArchive", "", false, ""));
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("NoWarnOnCreate", "", false, ""));
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("TruncateTimestamp", "", false, ""));
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("SuppressStartupBanner", "", false, ""));
                toolSwitch.ArgumentRelationList.Add(new ArgumentRelation("Verbose", "", false, ""));
                string[][] array = new string[][]
                {
                    new string[] { "Delete", "-d" },
                    new string[] { "Move", "-m" },
                    new string[] { "Print", "-p" },
                    new string[] { "Quick", "-q" },
                    new string[] { "Replacement", "-r" },
                    new string[] { "Table", "-t" },
                    new string[] { "Extract", "-x" }
                };
                toolSwitch.SwitchValue = base.ReadSwitchMap("Command", array, value);
                toolSwitch.Name = "Command";
                toolSwitch.Value = value;
                toolSwitch.MultipleValues = true;
                base.ActiveToolSwitches.Add("Command", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000D7 RID: 215
        // (get) Token: 0x06000346 RID: 838 RVA: 0x0000DA8E File Offset: 0x0000BC8E
        // (set) Token: 0x06000347 RID: 839 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
        [Required]
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
                toolSwitch.Separator = " ";
                toolSwitch.DisplayName = "Output File";
                toolSwitch.Description = "The /OUT option overrides the default name and location of the program that the lib creates.";
                toolSwitch.Required = true;
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Name = "OutputFile";
                toolSwitch.Value = value;
                base.ActiveToolSwitches.Add("OutputFile", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000D8 RID: 216
        // (get) Token: 0x06000348 RID: 840 RVA: 0x0000DB36 File Offset: 0x0000BD36
        // (set) Token: 0x06000349 RID: 841 RVA: 0x0000DB5C File Offset: 0x0000BD5C
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
                toolSwitch.Required = true;
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.TaskItemArray = value;
                base.ActiveToolSwitches.Add("Sources", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000D9 RID: 217
        // (get) Token: 0x0600034A RID: 842 RVA: 0x0000DBB2 File Offset: 0x0000BDB2
        // (set) Token: 0x0600034B RID: 843 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
        public virtual bool CreateIndex
        {
            get
            {
                return base.IsPropertySet("CreateIndex") && base.ActiveToolSwitches["CreateIndex"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("CreateIndex");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Create an archive index";
                toolSwitch.Description = "Create an archive index (cf. ranlib).  This can speed up linking and reduce dependency within its own library.";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "s";
                toolSwitch.Name = "CreateIndex";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("CreateIndex", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DA RID: 218
        // (get) Token: 0x0600034C RID: 844 RVA: 0x0000DC64 File Offset: 0x0000BE64
        // (set) Token: 0x0600034D RID: 845 RVA: 0x0000DC8C File Offset: 0x0000BE8C
        public virtual bool CreateThinArchive
        {
            get
            {
                return base.IsPropertySet("CreateThinArchive") && base.ActiveToolSwitches["CreateThinArchive"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("CreateThinArchive");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Create Thin Archive";
                toolSwitch.Description = "Create a thin archive.  A thin archive contains relativepaths to the objects instead of embedding the objects.  Switching between Thin and Normal requires deleting the existing library.";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "T";
                toolSwitch.Name = "CreateThinArchive";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("CreateThinArchive", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DB RID: 219
        // (get) Token: 0x0600034E RID: 846 RVA: 0x0000DD18 File Offset: 0x0000BF18
        // (set) Token: 0x0600034F RID: 847 RVA: 0x0000DD40 File Offset: 0x0000BF40
        public virtual bool NoWarnOnCreate
        {
            get
            {
                return base.IsPropertySet("NoWarnOnCreate") && base.ActiveToolSwitches["NoWarnOnCreate"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("NoWarnOnCreate");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "No Warning on Create";
                toolSwitch.Description = "Do not warn if when the library is created.";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "c";
                toolSwitch.Name = "NoWarnOnCreate";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("NoWarnOnCreate", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DC RID: 220
        // (get) Token: 0x06000350 RID: 848 RVA: 0x0000DDCC File Offset: 0x0000BFCC
        // (set) Token: 0x06000351 RID: 849 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
        public virtual bool TruncateTimestamp
        {
            get
            {
                return base.IsPropertySet("TruncateTimestamp") && base.ActiveToolSwitches["TruncateTimestamp"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("TruncateTimestamp");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Truncate Timestamp";
                toolSwitch.Description = "Use zero for timestamps and uids/gids.";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "D";
                toolSwitch.Name = "TruncateTimestamp";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("TruncateTimestamp", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DD RID: 221
        // (get) Token: 0x06000352 RID: 850 RVA: 0x0000DE80 File Offset: 0x0000C080
        // (set) Token: 0x06000353 RID: 851 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
        public virtual bool SuppressStartupBanner
        {
            get
            {
                return base.IsPropertySet("SuppressStartupBanner") && base.ActiveToolSwitches["SuppressStartupBanner"].BooleanValue;
            }
            set
            {
                base.ActiveToolSwitches.Remove("SuppressStartupBanner");
                ToolSwitch toolSwitch = new ToolSwitch(ToolSwitchType.Boolean);
                toolSwitch.DisplayName = "Suppress Startup Banner";
                toolSwitch.Description = "Dont show version number.";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.ReverseSwitchValue = "V";
                toolSwitch.Name = "SuppressStartupBanner";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("SuppressStartupBanner", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DE RID: 222
        // (get) Token: 0x06000354 RID: 852 RVA: 0x0000DF34 File Offset: 0x0000C134
        // (set) Token: 0x06000355 RID: 853 RVA: 0x0000DF5C File Offset: 0x0000C15C
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
                toolSwitch.DisplayName = "Verbose";
                toolSwitch.Description = "Verbose";
                toolSwitch.Parents.AddLast("Command");
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.SwitchValue = "v";
                toolSwitch.Name = "Verbose";
                toolSwitch.BooleanValue = value;
                base.ActiveToolSwitches.Add("Verbose", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

        // Token: 0x170000DF RID: 223
        // (get) Token: 0x06000356 RID: 854 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
        protected override ArrayList SwitchOrderList
        {
            get
            {
                return this.switchOrderList;
            }
        }

        // Token: 0x170000E0 RID: 224
        // (get) Token: 0x06000357 RID: 855 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
        // (set) Token: 0x06000358 RID: 856 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
        [Required]
        public string ProjectFileName { get; set; }

        // Token: 0x170000E2 RID: 226
        // (get) Token: 0x0600035B RID: 859 RVA: 0x0000E012 File Offset: 0x0000C212
        // (set) Token: 0x0600035C RID: 860 RVA: 0x0000E01A File Offset: 0x0000C21A
        public bool IsCompileUpToDate { get; set; }

        // Token: 0x170000E3 RID: 227
        // (get) Token: 0x0600035D RID: 861 RVA: 0x0000E023 File Offset: 0x0000C223
        // (set) Token: 0x0600035E RID: 862 RVA: 0x0000E02B File Offset: 0x0000C22B
        public bool CopyOutputFile { get; set; } = true;

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
                toolSwitch.Description = "Tracker log directory.";
                toolSwitch.ArgumentRelationList = new ArrayList();
                toolSwitch.Value = VCToolTask.EnsureTrailingSlash(value);
                base.ActiveToolSwitches.Add("TrackerLogDirectory", toolSwitch);
                base.AddActiveSwitchToolValue(toolSwitch);
            }
        }

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

        protected override ITaskItem[] TrackedInputFiles
        {
            get
            {
                return this.Sources;
            }
        }

        // Token: 0x04000139 RID: 313
        private ArrayList switchOrderList;
    }
}
