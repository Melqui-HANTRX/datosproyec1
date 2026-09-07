C#
using System;
using System.IO;

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