<script>
    function handleKeyPress(event) {
        if (event.key === 'Enter') {
        event.preventDefault(); // Ngăn chặn hành động mặc định của phím "Enter" (submit form)
    submitSearch(); // Gọi hàm xử lý tìm kiếm
        }
    }

    function submitSearch() {
        document.getElementById('searchForm').submit(); // Submit form
    }
</script>
