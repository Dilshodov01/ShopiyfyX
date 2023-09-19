using System.Media;
using ShopiyfyX.Service;
using ShopiyfyX.Service.Services;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Service.DTOs.UserDto;
using ShopiyfyX.Service.DTOs.OrderDto;
using ShopiyfyX.Service.Interfaces.User;
using ShopiyfyX.Service.DTOs.ProductDto;
using ShopiyfyX.Service.Interfaces.Order;
using ShopiyfyX.Service.DTOs.CategoryDto;
using ShopiyfyX.Service.DTOs.OrderItemDto;
using ShopiyfyX.Service.Interfaces.Product;
using ShopiyfyX.Service.Interfaces.Category;
using ShopiyfyX.Service.Interfaces.OrderItem;

namespace ShopiyfyX.Presentation.UI;

public class ProjectUI
{
    private static ICategoryService categoryService = new CategoryService();
    private static IOrderService orderService = new OrderService();
    private static IOrderItemService orderItemService = new OrderItemService();
    private static IProductService productService = new ProductService();
    private static IUserService userService = new UserService();

    public async Task PrintAsync()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(@"

                ████████████████████████████████████████████████████████████████████████████████████████████████████
                █▄─█▀▀▀█─▄█▄─▄▄─█▄─▄███─▄▄▄─█─▄▄─█▄─▀█▀─▄███─▄─▄─█─▄▄─███─▄▄▄▄█─█─█─▄▄─█▄─▄▄─█▄─▄█▄─▄▄─█▄─█─▄█▄─▀─▄█
                ██─█─█─█─███─▄█▀██─██▀█─███▀█─██─██─█▄█─██████─███─██─███▄▄▄▄─█─▄─█─██─██─▄▄▄██─███─▄████▄─▄███▀─▀██
                ▀▀▄▄▄▀▄▄▄▀▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▀▀▄▄▄▄▀▀▀▄▄▄▄▄▀▄▀▄▀▄▄▄▄▀▄▄▄▀▀▀▄▄▄▀▄▄▄▀▀▀▀▄▄▄▀▀▄▄█▄▄▀
");
        Console.WriteLine("Welcome to the ShopifyX");

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. User Management");
            Console.WriteLine("2. Category Management");
            Console.WriteLine("3. Product Management");
            Console.WriteLine("4. Order Management");
            Console.WriteLine("5. Order Item Management");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    await UserManagementMenu();
                    break;
                case "2":
                    await CategoryManagementMenu();
                    break;
                case "3":
                    await ProductManagementMenu();
                    break;
                case "4":
                    await OrderManagementMenu();
                    break;
                case "5":
                    await OrderItemManagementMenu();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


    // Category management
    private static async Task CategoryManagementMenu()
    {
        string loadAudio = "../../../../ShopiyfyX.Presentation/UI/qilich.wav";
        using (var loading = new SoundPlayer(loadAudio))
        {
            loading.Play();
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write("\t\t\t\t\t{0}% ", i);
                PrintProgressBar(i);
                System.Threading.Thread.Sleep(300);
                Console.CursorLeft = 0;
            }
            static void PrintProgressBar(int progress)
            {
                Console.Write("👑");
                int completedBars = progress / 10;
                for (int i = 0; i < 10; i++)
                {
                    if (i < completedBars)
                        Console.Write("🚁");

                    else
                        Console.Write(" ");
                }
                Console.Write("🤴");
            }
            loading.Stop();
        }
        Console.Clear();
        while (true)
        {
            Console.WriteLine("\nCategory Management Menu:");
            Console.WriteLine("1. Create Category");
            Console.WriteLine("2. View All Categories");
            Console.WriteLine("3. Delete Category");
            Console.WriteLine("4. Get By Id From Category");
            Console.WriteLine("5. Update Category");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    await CreateCategory();
                    break;
                case "2":
                    await ViewAllCategories();
                    break;
                case "3":
                    await DeleteCategories();
                    break;
                case "4":
                    await GetByIdCategories();
                    break;
                case "5":
                    await UpdateCategory();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateCategory()
    {
        try
        {
            CategoryForCreationDto categoryDto = new CategoryForCreationDto();

            Console.WriteLine("\nEnter Category Name:");
            categoryDto.Name = Console.ReadLine();

            var result = await categoryService.CreateAsync(categoryDto);
            Console.WriteLine($"Category created with ID: {result.Id}");
        }
        catch (ShopifyXException stEx)
        {
            Console.WriteLine($"Error ({stEx.StatusCode}): {stEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static async Task ViewAllCategories()
    {
        var categories = await categoryService.GetAllAsync();

        Console.WriteLine("\nList of Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine($"\nID: {category.Id},\nName: {category.Name}");
        }
    }

    private static async Task DeleteCategories()
    {
        try
        {
            Console.WriteLine("Enter the category Id");
            long id = long.Parse(Console.ReadLine());

            var deleteCategory = await categoryService.RemoveAsync(id);
            Console.WriteLine("Successfully deleted!");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task GetByIdCategories()
    {
        try
        {
            Console.WriteLine("Enter the category Id");
            long categoryId = long.Parse(Console.ReadLine());

            var getById = await categoryService.GetByIdAsync(categoryId);
            Console.WriteLine($"\nId: {getById.Id},\nCategory Name: {getById.Name}");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task UpdateCategory()
    {
        try
        {
            var updateDto = new CategoryForUpdateDto();

            Console.Write("Enter the category id: ");
            updateDto.Id = long.Parse(Console.ReadLine());
            Console.Write("Enter the category name: ");
            updateDto.Name = Console.ReadLine();

            var st = await categoryService.UpdateAsync(updateDto);
            Console.WriteLine("Category updated successfully!\n");
        }
        catch (ShopifyXException stEx)
        {
            Console.WriteLine($"Error ({stEx.StatusCode}): {stEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }


    // Order management
    private static async Task OrderManagementMenu()
    {
        string loadAudio = "../../../../ShopiyfyX.Presentation/UI/qilich.wav";
        using (var loading = new SoundPlayer(loadAudio))
        {
            loading.Play();
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write("\t\t\t\t\t{0}% ", i);
                PrintProgressBar(i);
                System.Threading.Thread.Sleep(300);
                Console.CursorLeft = 0;
            }
            static void PrintProgressBar(int progress)
            {
                Console.Write("👑");
                int completedBars = progress / 10;
                for (int i = 0; i < 10; i++)
                {
                    if (i < completedBars)
                        Console.Write("🚁");

                    else
                        Console.Write(" ");
                }
                Console.Write("🤴");
            }
            loading.Stop();
        }
        Console.Clear();
        while (true)
        {
            Console.WriteLine("\nOrder Management Menu:");
            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. View All Orders");
            Console.WriteLine("3. Delete Orders");
            Console.WriteLine("4. Get By Id Order");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    await CreateOrder();
                    break;
                case "2":
                    await ViewAllOrders();
                    break;
                case "3":
                    await DeleteOrder();
                    break;
                case "4":
                    await GetByIdOrder();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateOrder()
    {
        Console.WriteLine("\nEnter User ID for the Order:");
        if (long.TryParse(Console.ReadLine(), out long userId))
        {
            Console.WriteLine("\nEnter Total Amount:");
            if (decimal.TryParse(Console.ReadLine(), out decimal totalAmount))
            {
                OrderForCreationDto orderDto = new OrderForCreationDto
                {
                    UserId = userId,
                    TotalAmount = totalAmount,
                };

                try
                {
                    var result = await orderService.CreateAsync(orderDto);
                    Console.WriteLine($"Order created with ID: {result.Id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid Total Amount. Please enter a valid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid User ID. Please enter a valid number.");
        }
    }

    private static async Task DeleteOrder()
    {
        try
        {
            Console.WriteLine("Enter the order Id");
            long id = long.Parse(Console.ReadLine());

            var deleteOrder = await orderService.RemoveAsync(id);
            Console.WriteLine("Successfully deleted!");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task ViewAllOrders()
    {
        List<OrderForResultDto> orders = await orderService.GetAllAsync();

        Console.WriteLine("\nList of Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"\nID: {order.Id},\nUser ID: {order.UserId},\nTotal Amount: {order.TotalAmount}");
        }
    }

    private static async Task GetByIdOrder()
    {
        try
        {
            Console.WriteLine("Enter the order Id");
            long orderId = long.Parse(Console.ReadLine());

            var getById = await orderService.GetByIdAsync(orderId);
            Console.WriteLine($"\nId: {getById.Id},\nOrderID: {getById.UserId},\nUmumiyHarajat: {getById.TotalAmount}");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    // Order Item management
    private static async Task OrderItemManagementMenu()
    {
        string loadAudio = "../../../../ShopiyfyX.Presentation/UI/qilich.wav";
        using (var loading = new SoundPlayer(loadAudio))
        {
            loading.Play();
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write("\t\t\t\t\t{0}% ", i);
                PrintProgressBar(i);
                System.Threading.Thread.Sleep(300);
                Console.CursorLeft = 0;
            }
            static void PrintProgressBar(int progress)
            {
                Console.Write("👑");
                int completedBars = progress / 10;
                for (int i = 0; i < 10; i++)
                {
                    if (i < completedBars)
                        Console.Write("🚁");

                    else
                        Console.Write(" ");
                }
                Console.Write("🤴");
            }
            loading.Stop();
        }
        Console.Clear();
        while (true)
        {
            Console.WriteLine("\nOrder Item Management Menu:");
            Console.WriteLine("1. Create Order Item");
            Console.WriteLine("2. View All Order Items");
            Console.WriteLine("3. Delete Order Items");
            Console.WriteLine("4. Get BY Id Order Items");
            Console.WriteLine("5. Update Order Item");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    await CreateOrderItem();
                    break;
                case "2":
                    await ViewAllOrderItems();
                    break;
                case "3":
                    await DeleteOrderItem();
                    break;
                case "4":
                    await GetByIdOrderItem();
                    break;
                case "5":
                    await UpdateOrderItem();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateOrderItem()
    {
        Console.WriteLine("\nEnter Order ID for the Order Item:");
        if (long.TryParse(Console.ReadLine(), out long orderId))
        {
            Console.WriteLine("\nEnter Product ID:");
            if (long.TryParse(Console.ReadLine(), out long productId))
            {
                Console.WriteLine("\nEnter quantity: ");
                if (long.TryParse(Console.ReadLine(), out long quantitiy))
                {
                    OrderItemForCreationDto orderItemDto = new OrderItemForCreationDto()
                    {
                        OrderId = orderId,
                        ProductId = productId,
                    };

                    try
                    {
                        var result = await orderItemService.CreateAsync(orderItemDto);
                        Console.WriteLine($"Order Item created with ID: {result.Id}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Product ID. Please enter a valid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Order ID. Please enter a valid number.");
        }
    }

    private static async Task ViewAllOrderItems()
    {
        List<OrderItemForResultDto> orderItems = await orderItemService.GetAllAsync();

        Console.WriteLine("\nList of Order Items:");
        foreach (var orderItem in orderItems)
        {
            Console.WriteLine($"\nID: {orderItem.Id},\nOrder ID: {orderItem.OrderId},\nProduct ID: {orderItem.ProductId}");
        }
    }

    private static async Task DeleteOrderItem()
    {
        try
        {
            Console.WriteLine("Enter the orderItem Id");
            long id = long.Parse(Console.ReadLine());

            var deleteOrder = await orderItemService.RemoveAsync(id);
            Console.WriteLine("Successfully deleted!");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task GetByIdOrderItem()
    {
        try
        {
            Console.WriteLine("Enter the OrderItem Id");
            long orderId = long.Parse(Console.ReadLine());

            var getById = await orderItemService.GetByIdAsync(orderId);
            Console.WriteLine($"\nId: {getById.Id},\nOrderID: {getById.OrderId},\nProductID: {getById.ProductId}");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task UpdateOrderItem()
    {
        try
        {
            var updateDto = new OrderItemForUpdateDto();

            Console.Write("Enter the order item Id: ");
            updateDto.Id = long.Parse(Console.ReadLine());
            Console.Write("Enter the order id: ");
            updateDto.OrderId = long.Parse(Console.ReadLine());
            Console.Write("Enter the product id: ");
            updateDto.ProductId = long.Parse(Console.ReadLine());

            var st = await orderItemService.UpdateAsync(updateDto);
            Console.WriteLine("Order Item updated successfully!\n");
        }
        catch (ShopifyXException stEx)
        {
            Console.WriteLine($"Error ({stEx.StatusCode}): {stEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }


    // Product management
    private static async Task ProductManagementMenu()
    {
        string loadAudio = "../../../../ShopiyfyX.Presentation/UI/qilich.wav";
        using (var loading = new SoundPlayer(loadAudio))
        {
            loading.Play();
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write("\t\t\t\t\t{0}% ", i);
                PrintProgressBar(i);
                System.Threading.Thread.Sleep(300);
                Console.CursorLeft = 0;
            }
            static void PrintProgressBar(int progress)
            {
                Console.Write("👑");
                int completedBars = progress / 10;
                for (int i = 0; i < 10; i++)
                {
                    if (i < completedBars)
                        Console.Write("🚁");

                    else
                        Console.Write(" ");
                }
                Console.Write("🤴");
            }
            loading.Stop();
        }
        Console.Clear();
        while (true)
        {
            Console.WriteLine("\nProduct Management Menu:");
            Console.WriteLine("1. Create Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Delete Products");
            Console.WriteLine("4. Get By Id Products");
            Console.WriteLine("5. Update Product");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    await CreateProduct();
                    break;
                case "2":
                    await ViewAllProducts();
                    break;
                case "3":
                    await DeleteProduct();
                    break;
                case "4":
                    await GetByIdProduct();
                    break;
                case "5":
                    await UpdateProduct();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateProduct()
    {
        Console.WriteLine("\nEnter Product Name:");
        string productName = Console.ReadLine();

        Console.WriteLine("\nEnter Price:");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("\nEnter Description:");
            string description = Console.ReadLine();

            Console.WriteLine("\nEnter category id:");
            long categoryrId = long.Parse(Console.ReadLine());

            Console.WriteLine("\nEnter Quantity:");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                ProductForCreationDto productDto = new ProductForCreationDto
                {
                    Name = productName,
                    Price = price,
                    CategoryId = categoryrId,
                    Description = description,
                    Quantity = quantity
                };

                try
                {
                    var result = await productService.CreateAsync(productDto);
                    Console.WriteLine($"Product created with ID: {result.Id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid Quantity. Please enter a valid number.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Price. Please enter a valid number.");
        }
    }

    private static async Task ViewAllProducts()
    {
        List<ProductForResultDto> products = await productService.GetAllAsync();

        Console.WriteLine("\nList of Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"\nID: {product.Id},\nName: {product.Name},\nPrice: {product.Price},\nQuantity: {product.Quantity}");
        }
    }

    private static async Task DeleteProduct()
    {
        try
        {
            Console.WriteLine("Enter the Product Id");
            long id = long.Parse(Console.ReadLine());

            var deleteProduct = await productService.RemoveAsync(id);
            Console.WriteLine("Successfully deleted!");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task GetByIdProduct()
    {
        try
        {
            Console.WriteLine("Enter the Product Id");
            long productId = long.Parse(Console.ReadLine());

            var getById = await productService.GetByIdAsync(productId);
            Console.WriteLine($"\nId: {getById.Id},\nProduct name: {getById.Name},\nPrice: {getById.Price},\nQuantitiy: {getById.Quantity},\nDescription: {getById.Description}");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task UpdateProduct()
    {
        try
        {
            var updateDto = new ProductForUpdateDto();

            Console.Write("Enter the product id: ");
            updateDto.Id = long.Parse(Console.ReadLine());
            Console.Write("Enter the product name: ");
            updateDto.Name = Console.ReadLine();
            Console.Write("Enter the product price: ");
            updateDto.Price = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the product quantity: ");
            updateDto.Quantity = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter the category id: ");
            updateDto.CategoryId = long.Parse(Console.ReadLine());
            Console.WriteLine("Enter th product description: ");
            updateDto.Description = Console.ReadLine();

            var st = await productService.UpdateAsync(updateDto);
            Console.WriteLine("Product updated successfully!\n");
        }
        catch (ShopifyXException stEx)
        {
            Console.WriteLine($"Error ({stEx.StatusCode}): {stEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }


    // User management
    private static async Task UserManagementMenu()
    {
        string loadAudio = "../../../../ShopiyfyX.Presentation/UI/qilich.wav";
        using (var loading = new SoundPlayer(loadAudio))
        {
            loading.Play();
            for (int i = 0; i <= 100; i += 10)
            {
                Console.Write("\t\t\t\t\t{0}% ", i);
                PrintProgressBar(i);
                System.Threading.Thread.Sleep(300);
                Console.CursorLeft = 0;
            }
            static void PrintProgressBar(int progress)
            {
                Console.Write("👑");
                int completedBars = progress / 10;
                for (int i = 0; i < 10; i++)
                {
                    if (i < completedBars)
                        Console.Write("🚁");

                    else
                        Console.Write(" ");
                }
                Console.Write("🤴");
            }
            loading.Stop();
        }
        Console.Clear();
        while (true)
        {
            Console.WriteLine("\nUser Management Menu:");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. View All Users");
            Console.WriteLine("3. Delete Users");
            Console.WriteLine("4. Get by Id Users");
            Console.WriteLine("5. Update Users");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    await CreateUser();
                    break;
                case "2":
                    await ViewAllUsers();
                    break;
                case "3":
                    await DeleteUser();
                    break;
                case "4":
                    await GetByIdUser();
                    break;
                case "5":
                    await UpdateUser();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static async Task CreateUser()
    {
        Console.WriteLine("\nEnter First Name:");
        string firstName = Console.ReadLine();

        Console.WriteLine("\nEnter Last Name:");
        string lastName = Console.ReadLine();

        Console.WriteLine("\nEnter Email:");
        string email = Console.ReadLine();

        Console.WriteLine("\nEnter Password:");
        string password = Console.ReadLine();

        Console.WriteLine("\nEnter Phone Number:");
        string phoneNumber = Console.ReadLine();

        UserForCreationDto userDto = new UserForCreationDto()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password,
            PhoneNumber = phoneNumber
        };

        try
        {
            var result = await userService.CreateAsync(userDto);
            Console.WriteLine($"User created with ID: {result.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task ViewAllUsers()
    {
        List<UserForResultDto> users = await userService.GetAllAsync();

        Console.WriteLine("\nList of Users:");
        foreach (var user in users)
        {
            Console.WriteLine($"\nId: {user.Id},\nFirstname: {user.FirstName},\nLastname: {user.LastName},\nPhonenumber: {user.PhoneNumber}");
        }
    }

    private static async Task DeleteUser()
    {
        try
        {
            Console.WriteLine("Enter the User Id");
            long id = long.Parse(Console.ReadLine());

            var deleteUser = await userService.RemoveAsync(id);
            Console.WriteLine("Successfully deleted!");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task GetByIdUser()
    {
        try
        {
            Console.WriteLine("Enter the user Id");
            long usertId = long.Parse(Console.ReadLine());

            var getById = await userService.GetByIdAsync(usertId);
            Console.WriteLine($"\nId: {getById.Id},\nFirstname: {getById.FirstName},\nLastname: {getById.LastName},\nPhonenumber: {getById.PhoneNumber}");
        }
        catch (ShopifyXException st)
        {
            Console.WriteLine($"{st.StatusCode} {st.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static async Task UpdateUser()
    {
        try
        {
            var updateDto = new UserForUpdateDto();

            Console.Write("\nEnter the user Id: ");
            updateDto.Id = long.Parse(Console.ReadLine());
            Console.Write("\nEnter the first name: ");
            updateDto.FirstName = Console.ReadLine();
            Console.Write("\nEnter the last name: ");
            updateDto.LastName = Console.ReadLine();
            Console.WriteLine("\nEnter th phone number: ");
            updateDto.PhoneNumber = Console.ReadLine();

            var st = await userService.UpdateAsync(updateDto);
            Console.WriteLine("User updated successfully!\n");
        }
        catch (ShopifyXException stEx)
        {
            Console.WriteLine($"Error ({stEx.StatusCode}): {stEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}