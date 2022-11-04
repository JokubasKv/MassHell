using System;

namespace MassHell_Library
{
    // The base Component interface defines operations that can be altered by
    // decorators.
    public abstract class DrawComponent
    {
        public abstract string Draw();
    }

    // Concrete Components provide default implementations of the operations.
    // There might be several variations of these classes.
    class ConcreteComponent : DrawComponent
    {
        public override string Draw()
        {
            return "ConcreteComponent";
        }
    }

    // The base Decorator class follows the same interface as the other
    // components. The primary purpose of this class is to define the wrapping
    // interface for all concrete decorators. The default implementation of the
    // wrapping code might include a field for storing a wrapped component and
    // the means to initialize it.
    abstract class Decorator : DrawComponent
    {
        protected DrawComponent _component;

        public Decorator(DrawComponent component)
        {
            this._component = component;
        }

        public void SetComponent(DrawComponent component)
        {
            this._component = component;
        }

        // The Decorator delegates all work to the wrapped component.
        public override string Draw()
        {
            if (this._component != null)
            {
                return this._component.Draw();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // Concrete Decorators call the wrapped object and alter its result in some
    // way.
    class SkinDecorator : Decorator
    {
        public SkinDecorator(DrawComponent comp) : base(comp)
        {
        }

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Draw()
        {
            return $"ConcreteDecoratorA({base.Draw()})";
        }
    }

    // Decorators can execute their behavior either before or after the call to
    // a wrapped object.
    class labelDecorator : Decorator
    {
        public labelDecorator(DrawComponent comp) : base(comp)
        {
        }

        public override string Draw()
        {
            return $"ConcreteDecoratorB({base.Draw()})";
        }
    }

    public class Client
    {
        // The client code works with all objects using the Component interface.
        // This way it can stay independent of the concrete classes of
        // components it works with.
        public void ClientCode(DrawComponent component)
        {
            Console.WriteLine("RESULT: " + component.Draw());
        }
    }
}