C#
using System;
using System.IO;
using SistemaBiblioteca;

namespace SistemaBiblioteca
{
    // ==========================================
    // 1. MODELO DE DATOS
    // ==========================================
    public class Libro
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public int CopiasDisponibles { get; set; }
        public int VecesPrestado { get; set; }
        public bool Eliminado { get; set; } // Propiedad para la Baja Lógica

        public Libro(string codigo, string titulo, string autor, string categoria, int copias, int vecesPrestado = 0)
        {
            Codigo = codigo;
            Titulo = titulo;
            Autor = autor;
            Categoria = categoria;
            CopiasDisponibles = copias;
            VecesPrestado = vecesPrestado;
            Eliminado = false; // Por defecto está activo
        }

        public override string ToString()
        {
            return $"[{Codigo}] \"{Titulo}\" - {Autor} | Cat: {Categoria} | Copias: {CopiasDisponibles} | Préstamos: {VecesPrestado}";
        }
    }

    // ==========================================
    // 2. MAX HEAP (Top libros MÁS PRESTADOS)
    // ==========================================
    public class MaxHeapLibros
    {
        private Libro[] heap;
        public int Cantidad { get; private set; }

        public MaxHeapLibros(int capacidad = 100)
        {
            heap = new Libro[capacidad];
            Cantidad = 0;
        }

        private void Redimensionar()
        {
            Libro[] nuevoHeap = new Libro[heap.Length * 2];
            for (int i = 0; i < Cantidad; i++) nuevoHeap[i] = heap[i];
            heap = nuevoHeap;
        }

        public void Insertar(Libro libro)
        {
            if (Cantidad == heap.Length) Redimensionar();
            heap[Cantidad] = libro;
            Subir(Cantidad);
            Cantidad++;
        }

        private void Subir(int i)
        {
            while (i > 0)
            {
                int padre = (i - 1) / 2;
                if (heap[i].VecesPrestado > heap[padre].VecesPrestado)
                {
                    var temp = heap[i];
                    heap[i] = heap[padre];
                    heap[padre] = temp;
                    i = padre;
                }
                else break;
            }
        }

        public void Reestructurar()
        {
            for (int i = (Cantidad / 2) - 1; i >= 0; i--) Bajar(i);
        }

        private void Bajar(int i)
        {
            int mayor = i;
            int izq = 2 * i + 1;
            int der = 2 * i + 2;

            if (izq < Cantidad && heap[izq].VecesPrestado > heap[mayor].VecesPrestado) mayor = izq;
            if (der < Cantidad && heap[der].VecesPrestado > heap[mayor].VecesPrestado) mayor = der;

            if (mayor != i)
            {
                var temp = heap[i];
                heap[i] = heap[mayor];
                heap[mayor] = temp;
                Bajar(mayor);
            }
        }

        public void ImprimirTop(int k)
        {
            Reestructurar();
            int limite = Math.Min(k, Cantidad);
            Console.WriteLine($"\n--- TOP {limite} LIBROS MÁS PRESTADOS (Max Heap) ---");

            Libro[] copia = new Libro[Cantidad];
            for (int i = 0; i < Cantidad; i++) copia[i] = heap[i];
            int cantCopia = Cantidad;
            int impresos = 0;

            while (cantCopia > 0 && impresos < limite)
            {
                Libro actual = copia[0];
                copia[0] = copia[cantCopia - 1];
                cantCopia--;

                // Solo imprimimos si el libro NO está eliminado
                if (!actual.Eliminado)
                {
                    Console.WriteLine($"{impresos + 1}. {actual}");
                    impresos++;
                }

                int pos = 0;
                while (pos < cantCopia)
                {
                    int mayor = pos;
                    int izq = 2 * pos + 1;
                    int der = 2 * pos + 2;

                    if (izq < cantCopia && copia[izq].VecesPrestado > copia[mayor].VecesPrestado) mayor = izq;
                    if (der < cantCopia && copia[der].VecesPrestado > copia[mayor].VecesPrestado) mayor = der;

                    if (mayor != pos)
                    {
                        var t = copia[pos];
                        copia[pos] = copia[mayor];
                        copia[mayor] = t;
                        pos = mayor;
                    }
                    else break;
                }
            }
            if(impresos == 0) Console.WriteLine("No hay libros disponibles.");
        }
    }
}

// ==========================================
// 3. MIN HEAP (Listado Ordenado por TÍTULO de A-Z)
// ==========================================
public class MinHeapLibros
{
    private Libro[] heap;
        public int Cantidad { get; private set; }

        public MinHeapLibros(int capacidad = 100)
        {
            heap = new Libro[capacidad];
            Cantidad = 0;
        }

        private void Redimensionar()
        {
            Libro[] nuevoHeap = new Libro[heap.Length * 2];
            for (int i = 0; i < Cantidad; i++) nuevoHeap[i] = heap[i];
            heap = nuevoHeap;
        }
        public void Subir(init 1)
    {
        while (1 > 0 )
        {
                int padre = (i - 1) / 2;
                // Compara alfabéticamente el Título: Si i es menor que padre, sube
                if (string.Compare(heap[i].Titulo, heap[padre].Titulo, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    var temp = heap[i];
                    heap[i] = heap[padre];
                    heap[padre] = temp;
                    i = padre;
                }
                else break;
            }
        }

        public void Reestructurar()
        {
            for (int i = (Cantidad / 2) - 1; i >= 0; i--) Bajar(i);
        }

       private void Bajar(int i)
        {
            int menor = i;
            int izq = 2 * i + 1;
            int der = 2 * i + 2;

            if (izq < Cantidad && string.Compare(heap[izq].Titulo, heap[menor].Titulo, StringComparison.OrdinalIgnoreCase) < 0)
                menor = izq;

            if (der < Cantidad && string.Compare(heap[der].Titulo, heap[menor].Titulo, StringComparison.OrdinalIgnoreCase) < 0)
                menor = der;

            if (menor != i)
            {
                var temp = heap[i];
                heap[i] = heap[menor];
                heap[menor] = temp;
                Bajar(menor);
            }
        }
        public void ImprimirCatalogo()
        {
            Reestructurar();
            Console.WriteLine($"\n--- CATÁLOGO ORDENADO POR TÍTULO A-Z (Min Heap) ---");

            Libro[] copia = new Libro[Cantidad];
            for (int i = 0; i < Cantidad; i++) copia[i] = heap[i];
            int cantCopia = Cantidad;
            int contador = 1;

            while (cantCopia > 0)
            {
                Libro actual = copia[0];
                copia[0] = copia[cantCopia - 1];
                cantCopia--;

                if (!actual.Eliminado)
                {
                    Console.WriteLine($"{contador}. {actual}");
                    contador++;
                }

                int pos = 0;
                while (pos < cantCopia)
                {
                    int menor = pos;
                    int izq = 2 * pos + 1;
                    int der = 2 * pos + 2;

                    if (izq < cantCopia && string.Compare(copia[izq].Titulo, copia[menor].Titulo, StringComparison.OrdinalIgnoreCase) < 0) menor = izq;
                    if (der < cantCopia && string.Compare(copia[der].Titulo, copia[menor].Titulo, StringComparison.OrdinalIgnoreCase) < 0) menor = der;

                    if (menor != pos)
                    {
                        var t = copia[pos];
                        copia[pos] = copia[menor];
                        copia[menor] = t;
                        pos = menor;
                    }
                    else break;
                }
            }
            if (contador == 1) Console.WriteLine("El catálogo está vacío.");
        }
    }
}

    // ==========================================
    // 4. ÁRBOL B+
    // ==========================================
    public class NodoBPlus
    {
        public bool EsHoja;
        public string[] Claves;
        public Libro[] Datos;
        public NodoBPlus[] Hijos;
        public int NumClaves;
        public NodoBPlus SiguienteHoja;

        public NodoBPlus(int orden, bool esHoja)
        {
            EsHoja = esHoja;
            Claves = new string[orden];
            Datos = new Libro[orden];
            Hijos = new NodoBPlus[orden + 1];
            NumClaves = 0;
            SiguienteHoja = null;
        }
    }

    public class ArbolBPlus
    {
        private NodoBPlus raiz;
        private readonly int orden;

        public ArbolBPlus(int orden = 3)
        {
            this.orden = orden;
            raiz = new NodoBPlus(orden, true);
        }

        public Libro Buscar(string codigo)
        {
            return BuscarEnNodo(raiz, codigo);
        }

        private Libro BuscarEnNodo(NodoBPlus nodo, string codigo)
        {
            int i = 0;
            while (i < nodo.NumClaves && string.Compare(codigo, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) > 0) i++;

            if (nodo.EsHoja)
            {
                if (i < nodo.NumClaves && string.Equals(nodo.Claves[i], codigo, StringComparison.OrdinalIgnoreCase))
                {
                    //está eliminado lógicamente, se considera que no existe
                    if (!nodo.Datos[i].Eliminado) return nodo.Datos[i];
                }
                return null;
            }
            return BuscarEnNodo(nodo.Hijos[i], codigo);
        }

        public void Insertar(Libro libro)
        {
            NodoBPlus r = raiz;
            if (r.NumClaves == orden - 1)
            {
                NodoBPlus s = new NodoBPlus(orden, false);
                raiz = s;
                s.Hijos[0] = r;
                DividirHijo(s, 0, r);
                InsertarNoLleno(s, libro);
            }
            else
            {
                InsertarNoLleno(r, libro);
            }
        }

        private void InsertarNoLleno(NodoBPlus nodo, Libro libro)
        {
            int i = nodo.NumClaves - 1;
            if (nodo.EsHoja)
            {
                while (i >= 0 && string.Compare(libro.Codigo, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) < 0)
                {
                    nodo.Claves[i + 1] = nodo.Claves[i];
                    nodo.Datos[i + 1] = nodo.Datos[i];
                    i--;
                }
                nodo.Claves[i + 1] = libro.Codigo;
                nodo.Datos[i + 1] = libro;
                nodo.NumClaves++;
            }
            else
            {
                while (i >= 0 && string.Compare(libro.Codigo, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) < 0) i--;
                i++;
                if (nodo.Hijos[i].NumClaves == orden - 1)
                {
                    DividirHijo(nodo, i, nodo.Hijos[i]);
                    if (string.Compare(libro.Codigo, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) > 0) i++;
                }
                InsertarNoLleno(nodo.Hijos[i], libro);
            }
       }

        private void DividirHijo(NodoBPlus padre, int i, NodoBPlus hijo)
        {
            NodoBPlus z = new NodoBPlus(orden, hijo.EsHoja);
            int t = orden / 2;

            if (hijo.EsHoja)
            {
                z.NumClaves = hijo.NumClaves - t;
                for (int j = 0; j < z.NumClaves; j++)
                {
                    z.Claves[j] = hijo.Claves[j + t];
                    z.Datos[j] = hijo.Datos[j + t];
                }
                hijo.NumClaves = t;
                z.SiguienteHoja = hijo.SiguienteHoja;
                hijo.SiguienteHoja = z;
                for (int j = padre.NumClaves; j >= i + 1; j--) padre.Hijos[j + 1] = padre.Hijos[j];
                padre.Hijos[i + 1] = z;
                for (int j = padre.NumClaves - 1; j >= i; j--) padre.Claves[j + 1] = padre.Claves[j];
                padre.Claves[i] = z.Claves[0];
                padre.NumClaves++;
            }
            else
            {
                z.NumClaves = t - 1;
                for (int j = 0; j < z.NumClaves; j++) z.Claves[j] = hijo.Claves[j + t];
                for (int j = 0; j < t; j++) z.Hijos[j] = hijo.Hijos[j + t];
                hijo.NumClaves = t - 1;
                for (int j = padre.NumClaves; j >= i + 1; j--) padre.Hijos[j + 1] = padre.Hijos[j];
                padre.Hijos[i + 1] = z;
                for (int j = padre.NumClaves - 1; j >= i; j--) padre.Claves[j + 1] = padre.Claves[j];
                padre.Claves[i] = hijo.Claves[t - 1];
                padre.NumClaves++;
            }
        }
    }