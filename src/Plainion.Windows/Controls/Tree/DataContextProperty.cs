﻿
namespace Plainion.Windows.Controls.Tree
{
    class DataContextProperty<T>
    {
        private string myPropertyName;

        public DataContextProperty( string propertyName )
        {
            myPropertyName = propertyName;
        }

        public T Get( INode dataContext )
        {
            var prop = dataContext.GetType().GetProperty( myPropertyName );
            return (T)prop.GetValue( dataContext );
        }

        public void Set( INode dataContext, T value )
        {
            var prop = dataContext.GetType().GetProperty( myPropertyName );
            prop.SetValue( dataContext, value );
        }
    }
}
