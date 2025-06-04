self.addEventListener('install', function (event) {
    console.log('✅ Service Worker installed');
});

self.addEventListener('fetch', function (event) {
    // ممكن تضيفي كاش هنا لاحقاً
});
