import styles from "./Pagination.module.css";

// Pagination — near-verbatim from the ch.10 resource `pagination.jsx`.
// Renders null when there's nothing to page through, shows a windowed
// set of page buttons (current +/- 3) with first/last shortcuts and "..."
// ellipses, plus Prev/Next disabled at the boundaries.
function Pagination({ currentPage, totalPages, onPageChange }) {
  if (totalPages <= 1) return null;

  const getPageNumbers = () => {
    const start = Math.max(1, currentPage - 3);
    const end = Math.min(totalPages, currentPage + 3);
    const pages = [];
    for (let i = start; i <= end; i++) {
      pages.push(i);
    }
    return pages;
  };

  return (
    <nav className={styles.pagination}>
      <ul>
        <li className={currentPage === 1 ? styles.disabled : ""}>
          <button onClick={() => onPageChange(currentPage - 1)} disabled={currentPage === 1}>
            &lsaquo;
          </button>
        </li>

        {currentPage > 4 && (
          <>
            <li>
              <button onClick={() => onPageChange(1)}>1</button>
            </li>
            {currentPage > 5 && <li className={styles.dots}>...</li>}
          </>
        )}

        {getPageNumbers().map((page) => (
          <li key={page} className={page === currentPage ? styles.active : ""}>
            <button onClick={() => onPageChange(page)}>{page}</button>
          </li>
        ))}

        {currentPage < totalPages - 3 && (
          <>
            {currentPage < totalPages - 4 && <li className={styles.dots}>...</li>}
            <li>
              <button onClick={() => onPageChange(totalPages)}>{totalPages}</button>
            </li>
          </>
        )}

        <li className={currentPage === totalPages ? styles.disabled : ""}>
          <button onClick={() => onPageChange(currentPage + 1)} disabled={currentPage === totalPages}>
            &rsaquo;
          </button>
        </li>
      </ul>
    </nav>
  );
}

export default Pagination;
