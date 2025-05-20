// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    document.addEventListener('DOMContentLoaded', () => {
        const leafCount = 24; 
        const colors = ['green', 'orange', 'brown', 'darkgreen'];

        for (let i = 0; i < leafCount; i++) {
            const leaf = document.createElement('i');
            leaf.className = 'fas fa-leaf falling-leaf';

            leaf.style.left = Math.random() * window.innerWidth + 'px';
            leaf.style.color = colors[Math.floor(Math.random() * colors.length)];

            leaf.style.animationDuration = (5 + Math.random() * 5) + 's';
            leaf.style.animationDelay = (Math.random() * 5) + 's';

            document.body.appendChild(leaf);

            setTimeout(() => leaf.remove(), 11000);
        }
    });