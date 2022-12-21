using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MassHell_Library
{
    // The Originator holds some important state that may change over time. It
    // also defines a method for saving the state inside a memento and another
    // method for restoring the state from it.
    /*public class GameState
    {
        // For the sake of simplicity, the originator's state is stored inside a
        // single variable.
        private List<Player> players;

        public GameState(List<Player> state)
        {
            this.players = state;
            Console.WriteLine("Originator: My initial state is: " + state);
            players.ForEach(i => Console.Write("{0}: {1} {2}\t", i.Name, i.XCoordinate, i.YCoordinate));
        }

        // Saves the current state inside a memento.
        public IMemento Save()
        {
            return new ConcreteMemento(this.players);
        }

        // Restores the Originator's state from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this.players = memento.GetState();
            Console.WriteLine($"Originator: My state has changed to: ");
            players.ForEach(i => Console.Write("{0}: {1} {2}\t", i.Name, i.XCoordinate, i.YCoordinate));
        }
    }

    // The Memento interface provides a way to retrieve the memento's metadata,
    // such as creation date or name. However, it doesn't expose the
    // Originator's state.
    public interface IMemento
    {

        List<Player> GetState();

        DateTime GetDate();
    }

    // The Concrete Memento contains the infrastructure for storing the
    // Originator's state.
    class ConcreteMemento : IMemento
    {
        private List<Player> _state;

        private DateTime _date;

        public ConcreteMemento(List<Player> state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // The Originator uses this method when restoring its state.
        public List<Player> GetState()
        {
            return this._state;
        }


        public DateTime GetDate()
        {
            return this._date;
        }
    }

    // The Caretaker doesn't depend on the Concrete Memento class. Therefore, it
    // doesn't have access to the originator's state, stored inside the memento.
    // It works with all mementos via the base Memento interface.
    public class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private GameState _originator = null;

        public Caretaker(GameState originator)
        {
            this._originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetDate());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Here's the list of mementos:");

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetDate());
            }
        }
    }*/

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    public class Memento
    {
        public List<Player> players { get; set; }
        public Memento(List<Player> players)
        {
            this.players = players;
        }
        public string GetDetails()
        {
            return "Memento [players=" + String.Concat(players.Select(o => $" {o.Name}, {o.XCoordinate}, {o.YCoordinate} |")) + "]";
        }
    }
    public class Caretaker
    {
        private List<Memento> mementoList = new List<Memento>();
        public void AddMemento(Memento m)
        {
            mementoList.Add(m);
            Console.WriteLine("PlayerList snapshots Maintained by CareTaker :" + m.GetDetails());
        }
        public Memento UndoToMemento()
        {
            if (this.mementoList.Count == 0)
            {
                return null;
            }

            var memento = this.mementoList.Last();
            this.mementoList.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetDetails());

            return memento;
        }
        public void Getcare()
        {
            Console.WriteLine("GetCare");
            foreach (var item in mementoList)
            {
                Console.WriteLine(item.GetDetails());
            }
        }
    }

    public class Originator
    {
        public List<Player> players;

        public Memento CreateMemento()
        {
            return new Memento(players);
        }
        public void SetMemento(Memento memento)
        {
            players = memento.players;
        }
        public string GetDetails()
        {
            return String.Concat(players.Select(o => $" {o.Name}, {o.XCoordinate}, {o.YCoordinate} |"));
        }
    }
}


