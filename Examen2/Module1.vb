Module Module1

    Sub Main()
        Console.WriteLine("=== PRUEBAS RÁPIDAS CRUD CON INTERFACES ===")

        ' ============ DESCOMENTA LO QUE QUIERAS PROBAR ============

        ' Probar solo ProductoDAL con interfaz
        PruebaProductoRapida()

        'PruebaGenerarReporte()

        GenerarReporteVentas()


        ' Probar solo ClienteDAL con interfaz
        'PruebaClienteRapida()

        ' Probar solo VentaDAL con interfaz
        'PruebaVentaRapida()

        ' Probar solo VentaItemDAL con interfaz
        'PruebaVentaItemRapida()

        ' Probar TODO de una vez
        'PruebaTodoRapido()

        Console.WriteLine(vbCrLf & "Presiona ENTER para salir...")
        Console.ReadLine()
    End Sub

    Private Sub pruebaGenerarReporte()
        Console.WriteLine("🔹 GENERAR REPORTE DE VENTA")
        Try
            Dim ventaDAL As IVenta = New VentaImpl()
            Dim clienteDAL As ICliente = New ClienteImpl()
            ' Asegurar que hay una venta y un cliente
            Dim ventas = ventaDAL.BuscarTodas()
            If ventas.Count = 0 Then
                Console.WriteLine("⚠️ No hay ventas para generar reporte.")
                Return
            End If
            Dim venta = ventas.First()
            Dim cliente = clienteDAL.BuscarPorID(venta.idCliente)
            If cliente Is Nothing Then
                Console.WriteLine("⚠️ No se encontró el cliente para la venta.")
                Return
            End If
            ' Generar y mostrar reporte
            Dim reporte = ventaDAL.GeneraReporteVentas(venta, cliente)
            Console.WriteLine(reporte)
        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub PruebaProductoRapida()
        Console.WriteLine("🔹 PRODUCTO DAL CON INTERFAZ")
        Try
            ' Crear instancia usando la interfaz
            Dim productoDAL As IProducto = New ProductoImpl()

            ' INSERT
            Dim prod As New Producto(0, "Test Producto", 99.99, "Testing")
            Console.WriteLine($"INSERT: {productoDAL.Insertar(prod)}")

            ' SELECT ALL
            Dim productos = productoDAL.BuscarTodos()
            Console.WriteLine($"SELECT ALL: {productos.Count} productos")

            If productos.Count > 0 Then
                ' SELECT BY ID
                Dim primer = productoDAL.BuscarPorID(productos.First().id)
                Console.WriteLine($"SELECT BY ID: {primer IsNot Nothing}")

                ' UPDATE
                productos.Last().precio = 88.88
                Console.WriteLine($"UPDATE: {productoDAL.Actualizar(productos.Last())}")

                ' SELECT BY CATEGORY
                Dim testing = productoDAL.BuscarPorCategoria("Testing")
                Console.WriteLine($"SELECT BY CATEGORY: {testing.Count}")

                ' DELETE
                Console.WriteLine($"DELETE: {productoDAL.Eliminar(productos.Last().id)}")
            End If

        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub PruebaClienteRapida()
        Console.WriteLine("🔹 CLIENTE DAL CON INTERFAZ")
        Try
            ' Crear instancia usando la interfaz
            Dim clienteDAL As ICliente = New ClienteImpl()

            ' INSERT
            Dim cliente As New Cliente(0, "Test Cliente", "123-TEST", "test@test.com")
            Console.WriteLine($"INSERT: {clienteDAL.Insertar(cliente)}")

            ' SELECT ALL
            Dim clientes = clienteDAL.BuscarTodos()
            Console.WriteLine($"SELECT ALL: {clientes.Count} clientes")

            If clientes.Count > 0 Then
                ' SELECT BY ID
                Dim primer = clienteDAL.BuscarPorID(clientes.First().ID)
                Console.WriteLine($"SELECT BY ID: {primer IsNot Nothing}")

                ' UPDATE
                clientes.Last().Telefono = "999-UPDATED"
                Console.WriteLine($"UPDATE: {clienteDAL.Actualizar(clientes.Last())}")

                ' SELECT BY NAME
                Dim testing = clienteDAL.BuscarPorNombre("Test")
                Console.WriteLine($"SELECT BY NAME: {testing.Count}")

                ' DELETE
                Console.WriteLine($"DELETE: {clienteDAL.Eliminar(clientes.Last().ID)}")
            End If

        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub PruebaVentaRapida()
        Console.WriteLine("🔹 VENTA DAL CON INTERFAZ")
        Try
            ' Crear instancias usando las interfaces
            Dim ventaDAL As IVenta = New VentaImpl()
            Dim clienteDAL As ICliente = New ClienteImpl()

            ' Asegurar que hay un cliente
            Dim clientes = clienteDAL.BuscarTodos()
            If clientes.Count = 0 Then
                Dim clienteTemp As New Cliente(0, "Cliente Temp", "000", "temp@temp.com")
                clienteDAL.Insertar(clienteTemp)
                clientes = clienteDAL.BuscarTodos()
            End If

            ' INSERT (devuelve ID)
            Dim venta As New venta(0, clientes.First().ID, DateTime.Now, 150.0)
            Dim ventaID = ventaDAL.Insertar(venta)
            Console.WriteLine($"INSERT: ID generado = {ventaID}")

            If ventaID > 0 Then
                ' SELECT BY ID
                Dim ventaRecuperada = ventaDAL.BuscarPorID(ventaID)
                Console.WriteLine($"SELECT BY ID: {ventaRecuperada IsNot Nothing}")
            End If

            ' SELECT ALL
            Dim ventas = ventaDAL.BuscarTodas()
            Console.WriteLine($"SELECT ALL: {ventas.Count} ventas")

        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub PruebaVentaItemRapida()
        Console.WriteLine("🔹 VENTA ITEM DAL CON INTERFAZ")
        Try
            ' Crear instancias usando las interfaces
            Dim ventaItemDAL As IVentaItem = New VentaItemImpl()
            Dim ventaDAL As IVenta = New VentaImpl()
            Dim productoDAL As IProducto = New ProductoImpl()

            ' Asegurar que hay venta y producto
            Dim ventas = ventaDAL.BuscarTodas()
            Dim productos = productoDAL.BuscarTodos()

            If ventas.Count = 0 Or productos.Count = 0 Then
                Console.WriteLine("⚠️ Se necesita al menos 1 venta y 1 producto")
                Return
            End If

            ' INSERT
            Dim item As New VentaItem(0, ventas.First().id, productos.First().id, 50.0, 2)
            item.CalcularPrecioTotal() ' Total = 100.0
            Console.WriteLine($"INSERT: {ventaItemDAL.Insertar(item)} (Total: ${item.PrecioTotal})")

            ' SELECT BY VENTA
            Dim items = ventaItemDAL.BuscarPorVenta(ventas.First().id)
            Console.WriteLine($"SELECT BY VENTA: {items.Count} items")

            ' DELETE BY VENTA
            Console.WriteLine($"DELETE BY VENTA: {ventaItemDAL.EliminarPorVenta(ventas.First().id)}")

        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub GenerarReporteVentas()
        Console.WriteLine("🔹 GENERAR REPORTE DE VENTA")
        Try
            Dim ventaDAL As IVenta = New VentaImpl()
            Dim clienteDAL As ICliente = New ClienteImpl()
            ' Asegurar que hay una venta y un cliente
            Dim ventas = ventaDAL.BuscarTodas()
            If ventas.Count = 0 Then
                Console.WriteLine("⚠️ No hay ventas para generar reporte.")
                Return
            End If
            Dim venta = ventas.First()
            Dim cliente = clienteDAL.BuscarPorID(venta.idCliente)
            If cliente Is Nothing Then
                Console.WriteLine("⚠️ No se encontró el cliente para la venta.")
                Return
            End If
            ' Generar y mostrar reporte
            Dim reporte = ventaDAL.GeneraReporteVentas(venta, cliente)
            Console.WriteLine(reporte)
        Catch ex As Exception
            Console.WriteLine($"❌ Error: {ex.Message}")
        End Try
    End Sub

    Private Sub PruebaTodoRapido()
        Console.WriteLine("🚀 PROBANDO TODO RÁPIDAMENTE CON INTERFACES...")
        Console.WriteLine()

        PruebaProductoRapida()
        Console.WriteLine()

        PruebaClienteRapida()
        Console.WriteLine()

        PruebaVentaRapida()
        Console.WriteLine()

        PruebaVentaItemRapida()
        Console.WriteLine()

        Console.WriteLine("✅ TODAS LAS PRUEBAS CON INTERFACES COMPLETADAS")

        ' Resumen usando interfaces
        Try
            Console.WriteLine("📊 RESUMEN:")

            Dim productoDAL As IProducto = New ProductoImpl()
            Dim clienteDAL As ICliente = New ClienteImpl()
            Dim ventaDAL As IVenta = New VentaImpl()

            Console.WriteLine($"   Productos: {productoDAL.BuscarTodos().Count}")
            Console.WriteLine($"   Clientes: {clienteDAL.BuscarTodos().Count}")
            Console.WriteLine($"   Ventas: {ventaDAL.BuscarTodas().Count}")
        Catch
            Console.WriteLine("   (Error obteniendo resumen)")
        End Try
    End Sub
End Module