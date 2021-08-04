namespace PayrollSystem.Logic.Common
{
    internal abstract class Entity
    {
        public int ID { get; internal set; }

        public Entity(int id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity e)
                return false;

            if (ReferenceEquals(this, e))
                return true;

            if (GetType() != e.GetType())
                return false;

            if (ID == default || e.ID == default)
                return false;

            return ID == e.ID;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + ID).GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
