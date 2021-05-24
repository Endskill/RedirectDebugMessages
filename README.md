# RedirectDebugMessages
A GTFO Mod, which allows other Mods to redirect the Logging output to its own Tab in a different Application.

Make this Mod a SoftDependecy and from this point on you can add this Method somewhere and call it everytime, you want to log something.

 public static ExternalLogger GetExternalLogger()
        {
            try
            {
                return ExternalLogger.Current;
            }
            catch
            {
                return null;
            }
        }

So one example of using this Plugin would be:

GetExternalLogger()?.SendMessageAsync("My Logging Message", "TabName");

There are also some overloads for Color support like

GetExternalLogger()?.SendMessageAsync("My Logging Message", "TabName", UnityEngine.Color.cyan, UnityEngine.Color.red);
