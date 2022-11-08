#if (net5)
Serve.Run<FurionRazorApi.Web.Entry.Startup>(RunOptions.Default.WithArgs(args));
#else
Serve.Run(RunOptions.Default.WithArgs(args));
#endif