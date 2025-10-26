document.getElementById('registrationForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const fullName = document.getElementById('fullName').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    
    // Validação básica
    if (password !== confirmPassword) {
        alert('As senhas não coincidem. Por favor, verifique novamente.');
        return;
    }
    
    if (password.length < 8) {
        alert('A senha deve ter pelo menos 6 caracteres.');
        return;
    }
    
    // Aqui você normalmente enviaria os dados para o servidor
    alert('Cadastro realizado com sucesso! Bem-vindo(a) à INESCAFÉ!');
    
    // Limpar o formulário após o envio
    document.getElementById('registrationForm').reset();
});