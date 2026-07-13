# MSBuild LLVM Cross Compilation

This is an experimental project that reuses some of the code and props/targets for remote Linux builds to
locally cross compile for Linux and embedded targets with LLVM.

It supports a `RISCV64` platform (`riscv64-unknown-none`) and a `Generic` platform (user can specify any triple).
It should support executables, shared libraries, and static libraries, although only executables have been tested.
Linking to libraries should work as well, but has not been specifically tested. In general, this is largely reusing
existing code, and should work about as well as the remote Linux builds, just locally. It is somewhat patched
together, and is still an experiment.

All of these files, at least the parts that I did not modify or write, are Microsoft's code. The rest is my own
modification.

## Instructions

You will need to build the `BuildExt.Cpp.Clang` project, and then make a symlink/NTFS junction to put the `LLVMCross`
folder under `C:\Program Files\Microsoft Visual Studio\2022\<edition>\MSBuild\Microsoft\VC\v170\Application Type`

To make your project work, compare `testproj.vcxproj` to a stock Linux vcxproj. The following are the major changes:

- `Keyword` and `ApplicationType` are set to `LLVMCross` (this needs to be done by hand)
- `Platform` is `Generic` or `RISCV64` instead of `ARM64`, `x64`, etc (can be done, albeit tediously, in the GUI)
- `PlatformToolset` is set to `Clang` (should autopopulate in the GUI? just select if not)
- In the `Generic` platform sections, `LLVMTarget` is used to specify the triple (can be set in the GUI)

## Notes

The C# code here is patched together from some decompiled MSBuild code found around the folder mentioned in the instructions,
from `Microsoft.Build.CppTasks.Common.dll` and `Application Type\Linux\1.0\Microsoft.Build.Linux.Tasks.dll`. There are
also various `.props`, `.targets`, and `.xml` files around that are largely reused from the Linux ones and `Microsoft.Cpp.Clang.*`.

The `.props` files mostly define variables, `.targets` define specific tasks to invoke and their settings, and `.xml`
files under the `1033` folder (or whatever language ID) define the GUI property pages.

`BuildExt.Cpp.Clang.*` are based on the `Microsoft.Cpp.Clang.*` ones, the easiest way to see what I've done is compare them,
and also you can decompile the C# DLLs in [dnSpyEx](https://github.com/dnSpyEx/dnSpy) to compare further.

More needs to be documented, tested, and polished in general, but this should largely work for most situations in theory.
