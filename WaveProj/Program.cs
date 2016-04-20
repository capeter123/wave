using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using RestSharp;

namespace WaveProj
{
	class Program
	{
		static void Main(string[] args)
		{
			
		}

		private static void WaveOAuth()
		{
			string ulr = "https://eu6.salesforce.com/services/data/v36.0/wave";
			string urlForAuth = "https://login.salesforce.com/services/oauth2/token";
			var headersForAuth = new Dictionary<string, string>
			{
				{"client_id", MagicValues.Key},
				{"client_secret", MagicValues.Secret},
				{"grant_type", "password"},
				{"username", "waveroman@softheme.com"},
				{"password", "19kapusta87QEzK1qzEMwDsV57iraC66PcZV"}
			};

			var headers = new Dictionary<string, string>
			{
				{"Authorization", "Bearer 00D58000000ZoKk!AQ0AQG.U2cpD_fJnDYqAxpfAojQnBlM_PEH_vf_KEJwuO92BjbwhKTXSBGVrSbzU.F4CbiUc9y_Frrgpi7AjtL_c51OyHhtY"}
			};

			Rest(ulr, headers);
		}

		private static void Rest(string url, Dictionary<string, string> headers)
		{
			var restClient = new RestClient(url);
			var restRequest = new RestRequest(Method.GET);

			foreach (var header in headers)
			{
				restRequest.AddHeader(header.Key, header.Value);
			}

			IRestResponse restResponse = restClient.Execute(restRequest);

			var deserializeObject = JsonConvert.DeserializeObject<WaveResource>(restResponse.Content);

			Console.WriteLine(restResponse.Content);
		}
	}

	public class WaveResource
	{
		public string Datasets { get; set; }
		public string Lenses { get; set; }
		public string Query { get; set; }
	}

	public class FoldersListResource
	{
		public Folder[] Folders { get; set; }
		public string NextPageUrl { get; set; }
		public int TotalSize { get; set; }
		public string Url { get; set; }
	}

	public class Folder
	{
		public ApplicationStatus ApplicationStatus { get; set; }
		public string AssetSharingUrl { get; set; }
		public WaveUser CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Description { get; set; }
		public AssetReferenceRepresentation Icon { get; set; }
		public string Id { get; set; }
		public string Label { get; set; }
		public DateTime LastAccessDate { get; set; }
		public WaveUser LastModifiedBy { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public string Name { get; set; }
		public string Namespace { get; set; }
		public Permissions Permissions { get; set; }
		public WaveFolderShare[] Shares { get; set; }
		public string TemplateSourceId { get; set; }
		public string Type { get; set; }
		public string Url { get; set; }
	}

	public class WaveFolderShare
	{
		public string AccessType { get; set; }
		public string Url { get; set; }
		public string ImageUrl { get; set; }
		public ShateType ShateType { get; set; }
		public string SharedWithId { get; set; }
		public string SharedWithLabel { get; set; }
	}

	public enum ShateType
	{
		AllCspUsers,
		AllPrmUsers,
		CustomerPortalUser,
		Group,
		Organization,
		PartnerUser,
		PortalRole,
		PortalRoleAndSubordinates,
		Role,
		RoleAndSubordinates,
		User
	}

	public class Permissions
	{
		public bool Manage { get; set; }
		public bool Modify { get; set; }
		public bool View { get; set; }
	}

	public class AssetReferenceRepresentation
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string Label { get; set; }
	}

	public enum ApplicationStatus
	{
		CancelledStatus,
		CompletedStatus,
		DataflowInProgressStatus,
		FailedStatus,
		InProgressStatus,
		NewStatus
	}

	public class WaveUser
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ProfilePhotoUrl { get; set; }
	}
}
