using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRabbitMQ.Receiver
{
    public static class WebApi
	{
		public static async Task<string> PutAsync<T>(string api, T obj)
		{
			string result = string.Empty;
			try
			{
				result = await GetPutResultAsync(api, obj);
			}
			catch (Exception exp)
			{
				Log.Warning(exp, $"Url: {api}");
			}

			return result;
		}


		public static async Task<string> PostAsync<T>(string api, T obj)
		{
			string result = string.Empty;
			try
			{
				result = await GetPostResultAsync(api, obj);
			}
			catch (Exception exp)
			{
				Log.Warning(exp, $"Url: {api}");
			}

			return result;
		}


		private static async Task<string> GetPutResultAsync<T>(string api, T obj)
		{
			using (HttpClient client = new HttpClient(new HttpClientHandler { UseProxy = false, UseDefaultCredentials = true }))
			{
				var response = await client.PutAsync(api, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
				try
				{
					response.EnsureSuccessStatusCode();
					string result = await response.Content.ReadAsStringAsync();
					return result;

				}
				catch (Exception exp)
				{
					Log.Warning(exp, $"Url: {api}");
					return string.Empty;
				}

			}
		}

		private static async Task<string> GetPostResultAsync<T>(string api, T obj)
		{
			using (HttpClient client = new HttpClient(new HttpClientHandler { UseProxy = false, UseDefaultCredentials = true }))
			{
				var response = await client.PostAsync(api, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
				try
				{
					response.EnsureSuccessStatusCode();
					string result = await response.Content.ReadAsStringAsync();
					return result;

				}
				catch(Exception exp)
				{
					Log.Warning(exp, $"Url: {api}");
					return string.Empty;
				}

			}
		}
	}
}
