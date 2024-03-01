//This is the correct use for the httpclient but even doing everything by the book WSACleanup and WSAStartup errors kept apearing.
  string apiResponse = "";
  using (var client = new RestApiClient("https://xxx.xxxx.com"))
  {
       apiResponse = await client.CallApi(httpMethod, relativePath, jsonInput);
  }
