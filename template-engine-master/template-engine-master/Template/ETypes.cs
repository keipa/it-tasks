using System;

namespace TemplateEngine
{
	public class TemplateFormatException : Exception
	{
		public TemplateFormatException(string message)
		{
		}
	}

	class RemoteServiceException : Exception
	{
		public RemoteServiceException(string message)
		{
		}
	}

	public class BadCodeException : Exception
	{
		public BadCodeException(string message)
		{
		}
	}
}

