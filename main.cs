// NuGet Library: AWSSDK.SimpleEmailV2

static async Task Main()
{
	var accessKey = "amazonSes_accessKey";
	var secretKey = "amazonSes_secretKey";
	var region = "eu-north-1";
	
	var subject = "email-subject";
	var message = "<p>This is a <b>test</b> email.</p>
	var senderAddress = "info@datakent.com";
	var toAddress = "murat.turan@datakent.com";

	var client = new AmazonSimpleEmailServiceV2Client(accessKey, secretKey,
		RegionEndpoint.GetBySystemName(region));

	var attachmentList = new List<Amazon.SimpleEmailV2.Model.Attachment>();

	attachmentList.Add(new Amazon.SimpleEmailV2.Model.Attachment
	{
		FileName = "test.pdf",
		RawContent = new MemoryStream(File.ReadAllBytes("d:/file/path/test.pdf")),
		ContentType = "application/octet-stream",
		ContentTransferEncoding = AttachmentContentTransferEncoding.BASE64
	});

	var sendRequest = new Amazon.SimpleEmailV2.Model.SendEmailRequest
	{
		FromEmailAddress = senderAddress,
		Destination = new Amazon.SimpleEmailV2.Model.Destination
		{
			ToAddresses = new List<string> { toAddress }
		},
		Content = new Amazon.SimpleEmailV2.Model.EmailContent
		{
			Simple = new Amazon.SimpleEmailV2.Model.Message
			{
				Attachments = attachmentList,
				Subject = new Amazon.SimpleEmailV2.Model.Content
				{
					Charset = "UTF-8",
					Data = subject
				},
				Body = new Amazon.SimpleEmailV2.Model.Body
				{
					Html = new Amazon.SimpleEmailV2.Model.Content
					{
						Charset = "UTF-8",
						Data = message
					}
				}
			}
		}
	};

	try
	{
		var response = client.SendEmailAsync(sendRequest).Result;
		Console.WriteLine(response.MessageId);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
}
