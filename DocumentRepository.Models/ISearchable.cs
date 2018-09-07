using System.Data;

namespace DocumentRepository.Models
{
	public interface ISearchable
	{
		DataTable ByUploaded(int DiaryNumber);
		DataTable ByUploaded(int DiaryNumber, string CertLastName);
		DataTable ByUploaded(int DiaryNumber, int CertEdipi);
		DataTable ByUploaded(int DiaryNumber, string CertLastName, int CertEdipi);
		DataTable ByUploader(string UploadUser);
		DataTable ByUploader(string UploadUser, int DiaryNumber);
		DataTable ByUploadCert(string CertLastName);
		DataTable ByUploadCert(int CertEdipi);
		DataTable ByNotUploaded(int DiaryNumber);
		DataTable ByNotUploaded(int DiaryNumber, string CertLastName);
		DataTable ByNotUploaded(int DiaryNumber, int CertEdipi);
		DataTable ByNotUploaded(int DiaryNumber, int CertEdipi, string CertLastName);
		DataTable ByNotUploadedCert(string CertLastName);
		DataTable ByNotUploadedCert(int CertEdipi);
	}
}
