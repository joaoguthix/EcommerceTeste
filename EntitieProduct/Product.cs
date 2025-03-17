using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EntitieProduct
{
    public class Product
    {
        [Key]
        private int _codigo;
        private string _cnojFornecedor;
        private string _descricao;
        private string _situacao;
        private DateTime _dataFabricacao;
        private DateTime _dataValidade;
        private string _codigoFornecedor;
        private string _descricaoFornecedor;
        private int _quantidade;

        public Product(){}

        [Key]
        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Descrição não pode ser vazia");
                }
                _descricao = value;
            }
        }

        public string Situacao
        {
            get { return _situacao; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Situação não pode ser vazia");
                }
                _situacao = value;
            }
        }

        public DateTime DataFabricacao
        {
            get { return _dataFabricacao; }
            set
            {
                _dataFabricacao = value;
            }
        }

        public DateTime DataValidade
        {
            get { return _dataValidade; }
            set
            {
                _dataValidade = value;
            }
        }

        public string CodigoFornecedor
        {
            get { return _codigoFornecedor == null ? "" : _codigoFornecedor; }
            set { _codigoFornecedor = value; }
        }

        public string DescricaoFornecedor
        {
            get { return _descricaoFornecedor == null ? "" : _descricaoFornecedor; }
            set { _descricaoFornecedor = value; }
        }

        public string CNPJFornecedor
        {
            get { return _cnojFornecedor; }
            set
            {
                _cnojFornecedor = value;
            }
        }
        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                _quantidade = value;
            }
        }
    }
}
