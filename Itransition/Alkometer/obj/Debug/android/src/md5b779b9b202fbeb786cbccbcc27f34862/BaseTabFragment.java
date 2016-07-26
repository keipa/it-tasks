package md5b779b9b202fbeb786cbccbcc27f34862;


public class BaseTabFragment
	extends android.app.Fragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Alkometer.Activities.BaseTabFragment, Alkometer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseTabFragment.class, __md_methods);
	}


	public BaseTabFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseTabFragment.class)
			mono.android.TypeManager.Activate ("Alkometer.Activities.BaseTabFragment, Alkometer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
