document.getElementById('registrationForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const fullName = document.getElementById('fullName').value;
    const email = document.getElementById('email').value;
    
    // Validação básica
    if ((fullName == null)&&(email == null)) {
        alert('Preencha os dois campos para seguir com o cadastro');
        return;
    }
    
    // Aqui você normalmente enviaria os dados para o servidor
    alert('Cadastro realizado com sucesso! Bem-vindo(a) à INESCAFÉ!');
    
    // Limpar o formulário após o envio
    document.getElementById('registrationForm').reset();
});