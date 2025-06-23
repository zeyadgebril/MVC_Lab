    function togglePassword() {
            const passwordInput = document.getElementById('password');
    const toggleIcon = document.getElementById('toggleIcon');

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
    toggleIcon.classList.remove('fa-eye');
    toggleIcon.classList.add('fa-eye-slash');
            } else {
        passwordInput.type = 'password';
    toggleIcon.classList.remove('fa-eye-slash');
    toggleIcon.classList.add('fa-eye');
            }
        }

   ('submit', function(e) {
        e.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const submitBtn = document.querySelector('.btn-login');
    const loading = document.querySelector('.loading');

            document.querySelectorAll('.error-message').forEach(error => {
        error.classList.remove('show');
            });

    let isValid = true;

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        document.getElementById('emailError').classList.add('show');
    isValid = false;
            }

    if (password.length < 6) {
        document.getElementById('passwordError').classList.add('show');
    isValid = false;
            }

    if (isValid) {
        loading.classList.add('show');
    submitBtn.disabled = true;
    submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin loading show"></i>Signing In...';

                setTimeout(() => {
        loading.classList.remove('show');
    submitBtn.disabled = false;
    submitBtn.innerHTML = 'Sign In';

    alert('Login successful! Redirecting to dashboard...');
                }, 2000);
            }
        });

        document.querySelectorAll('.form-control').forEach(input => {
        input.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

    input.addEventListener('blur', function() {
                if (this.value === '') {
        this.parentElement.classList.remove('focused');
                }
            });
        });

        document.querySelectorAll('.btn-login, .social-btn').forEach(button => {
        button.addEventListener('click', function (e) {
            const ripple = document.createElement('span');
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
            const x = e.clientX - rect.left - size / 2;
            const y = e.clientY - rect.top - size / 2;

            ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
            ripple.classList.add('ripple');

            this.appendChild(ripple);

            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
        });

    const style = document.createElement('style');
    style.textContent = `
    .ripple {
        position: absolute;
    border-radius: 50%;
    background: rgba(255, 255, 255, 0.3);
    transform: scale(0);
    animation: rippleAnimation 0.6s linear;
    pointer-events: none;
            }

    @keyframes rippleAnimation {
        to {
        transform: scale(4);
    opacity: 0;
                }
            }
    `;
    document.head.appendChild(style);
