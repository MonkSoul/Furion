#if (net5)
Serve.Run<FurionBlazorApi.Web.Entry.Startup>(RunOptions.Default.WithArgs(args));
#else
Serve.Run(RunOptions.Default.WithArgs(args));
#endif