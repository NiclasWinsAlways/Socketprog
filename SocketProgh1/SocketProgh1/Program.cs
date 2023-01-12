//Starts server on different thread
new Thread(new ThreadStart(new Server().ExecuteServer)).Start();
//Starts client on normal thread
new Client().StartClient(); ;