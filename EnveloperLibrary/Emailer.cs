using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnveloperLibrary
{
    /// <summary>
    /// Funções para montar e enviar email
    /// </summary>
    public class Emailer
    {
        // emailDestino endereço destinatário
        public void Enviar(string emailDestino, string emailTitulo,  string emailMensagem, string emailAnexo = "")
        {
            // email remetente
            string _emailOrigem = "gelateriaperdonato@gmail.com";
            string _emailOrigemPass = "A13GelatoE30";
            
            // cria uma mensagem
            MailMessage correio = new MailMessage();

            // define os endereços
            correio.From = new MailAddress(_emailOrigem);
            correio.To.Add(new MailAddress(emailDestino));

            //define conteúdo
            correio.Subject = emailTitulo;
            correio.Body = emailMensagem;

            // verifica se tem caminho para arquivo anexo
            if (emailAnexo != "")
            {
                Attachment arquivoAnexo = new Attachment(emailAnexo);
                correio.Attachments.Add(arquivoAnexo);
            }

            //enviar a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_emailOrigem, _emailOrigemPass);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(correio);

            smtp.Dispose();
            correio.Dispose();

        }
    }
}
