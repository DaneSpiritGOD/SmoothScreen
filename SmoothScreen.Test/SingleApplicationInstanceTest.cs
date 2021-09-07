using NUnit.Framework;

namespace SmoothScreen.Test
{
	public class Tests
	{
		[Test]
		public void TestCanEnter()
		{
			Assert.That(SingleApplicationInstance.CanEnter(out var disposable), Is.True);
			Assert.That(disposable, Is.Not.Null);
			disposable.Dispose();
		}

		[Test]
		public void TestCannotEnter()
		{
			SingleApplicationInstance.CanEnter(out var disposable);

			try
			{ 
				Assert.That(SingleApplicationInstance.CanEnter(out var disposable2), Is.False);
				Assert.That(disposable2, Is.Null);
			}
			finally
			{
				disposable.Dispose();
			}
		}
	}
}