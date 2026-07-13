# MSBuild LLVM Cross Compilation

This is an experimental project that reuses some of the code and props/targets for remote Linux builds to
locally cross compile for Linux and embedded targets with LLVM.

It supports a `RISCV64` platform (`riscv64-unknown-none`) and a `Generic` platform (user can specify any triple).
It should support executables, shared libraries, and static libraries, although only executables have been tested.
Linking to libraries should work as well, but has not been specifically tested. In general, this is largely reusing
existing code, and should work about as well as the remote Linux builds, just locally. It is somewhat patched
together, and is still an experiment.

