using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSorting
{
    public class Animals : IEnumerable<Animal>
    {
        private Animal[] animals;

        public Animals(Animal[] animals)
        {
            if (animals == null)
                throw new ArgumentNullException("animals array can't be null");

            this.animals = animals;
        }
        public IEnumerator<Animal> GetEnumerator()
        {
            return new Enumerator(this.animals);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        class Enumerator : IEnumerator<Animal>
        {
            private int position;
            private Animal[] animals;

            public Animal Current { get => animals[position]; }

            object IEnumerator.Current => this.Current;

            public Enumerator(Animal[] animals)
            {
                this.position = -1;
                this.animals = animals;
            }

            public void Dispose()
            {
                this.Reset();
            }

            public bool MoveNext()
            {
                position++;
		return position < this.animals.Length;
            }

            public void Reset()
            {
                this.position = 0;
            }
        }
    }
}
