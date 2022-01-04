using System.Data;
using System.Data.Objects;

namespace HConnexum.Base.Datos
{
	public static class ExtensionesEntityFramework
	{
		public static bool IsAttachedTo(this ObjectContext context, object entity)
		{
			ObjectStateEntry entry;
			bool isAttached = false;
			if (context.ObjectStateManager.TryGetObjectStateEntry(context.CreateEntityKey(entity.GetType().Name, entity), out entry))
				isAttached = entry.State != EntityState.Detached;
			else
				isAttached = false;
			return isAttached;
		}
	}
}