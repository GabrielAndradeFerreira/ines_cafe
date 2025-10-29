// Adicione ao seu script.js
document.addEventListener('DOMContentLoaded', function() {
    const formulario = document.getElementById('formulario-cadastro');
    
    formulario.addEventListener('submit', function(e) {
        e.preventDefault();
        
        // Coletar todos os dados do formulário
        const formData = new FormData(formulario);
        const dados = Object.fromEntries(formData.entries());
        
        // Validação adicional
        if (!validarFormulario(dados)) {
            return;
        }
        
        // Enviar para o backend
        enviarParaBanco(dados);
    });
    
    function validarFormulario(dados) {
        // Validação de CPF
        if (!validarCPF(dados.cpf.replace(/\D/g, ''))) {
            alert('Por favor, insira um CPF válido.');
            return false;
        }
        
        // Validação de email
        if (!validarEmail(dados.email)) {
            alert('Por favor, insira um email válido.');
            return false;
        }
        
        // Validação de idade mínima (18 anos)
        if (!validarIdade(dados.nascimento)) {
            alert('Você deve ter pelo menos 18 anos para se cadastrar.');
            return false;
        }
        
        return true;
    }
    
    function validarCPF(cpf) {
        // Implementação básica de validação de CPF
        if (cpf.length !== 11) return false;
        return true;
    }
    
    function validarEmail(email) {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(email);
    }
    
    function validarIdade(dataNascimento) {
        const nascimento = new Date(dataNascimento);
        const hoje = new Date();
        const idade = hoje.getFullYear() - nascimento.getFullYear();
        const mes = hoje.getMonth() - nascimento.getMonth();
        
        if (mes < 0 || (mes === 0 && hoje.getDate() < nascimento.getDate())) {
            return idade - 1 >= 18;
        }
        return idade >= 18;
    }
    
    function enviarParaBanco(dados) {
        // Simulação de envio para o backend
        console.log('Dados para cadastro:', dados);
        
        // Aqui você faria a requisição AJAX para seu backend
        // Exemplo com fetch:
        /*
        fetch('/api/cadastrar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(dados)
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Cadastro realizado com sucesso!');
                // Redirecionar ou limpar formulário
            } else {
                alert('Erro no cadastro: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Erro:', error);
            alert('Erro ao conectar com o servidor.');
        });
        */
        
        // Simulação de sucesso
        alert('Cadastro realizado com sucesso! Em breve você receberá atualizações sobre sua Prensa Francesa INESCAFÉ Elegance.');
        formulario.reset();
    }
    
    // Máscaras (mantenha as que já tinha para CPF e telefone)
    // ... suas máscaras existentes
});