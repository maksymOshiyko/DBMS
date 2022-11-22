import React, { useState, useCallback, useEffect } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronDown, faSpinner, faX } from '@fortawesome/free-solid-svg-icons';

export const Table = () => {
  const [table, setTable] = useState({});
  const [tableIndex, setTableIndex] = useState();
  const [sortColumnIndex, setSortColumnIndex] = useState();
  const [isLoading, setIsLoading] = useState(true);

  const fetchTable = useCallback((tIndex, sortColIndex) => {
    setIsLoading(true);
    return fetch(
      `/Tables/SortByColumn?tableIndex=${tIndex}&sortColumnIndex=${sortColIndex}`
    )
      .then((res) => res.json())
      .then((data) => {
        setTable(data);
        setIsLoading(false);
      });
  }, []);

  useEffect(() => {
    console.log(1);
    const params = new Proxy(new URLSearchParams(window.location.search), {
      get: (searchParams, prop) => searchParams.get(prop),
    });
    setTableIndex(params.tableIndex);

    fetchTable(params.tableIndex);
  }, [fetchTable]);

  const sortTable = (index) => {
    setSortColumnIndex(index);
    fetchTable(tableIndex, index);
  };

  const getRow = (row) => {
    const isPng = (index) => {
      return table.columns[index].type === "PNG";
    };

    return (
      <tr>
        {table.columns.map((col) => (
          <td key={`${row.index}${col.index}`}>
            {isPng(col.index) ? (
              <a
                href={`/Rows/Png?rowIndex=${row.index}&tableIndex=${tableIndex}&columnIndex=${col.index}`}
              >
                {row.values[col.index] ? 'View image' : ''}
              </a>
            ) : (
              row.values[col.index] || ''
            )}
          </td>
        ))}
        <td className="text-end">
          <a
            className="btn btn-primary btn-sm me-3"
            href={`/Rows/EditRow?tableIndex=${tableIndex}&rowIndex=${row.index}`}
          >
            Edit
          </a>
          <a
            className="btn btn-danger btn-sm"
            href={`/Rows/DeleteRow?tableIndex=${tableIndex}&rowIndex=${row.index}`}
          >
            Delete
          </a>
        </td>
      </tr>
    );
  };

  if (isLoading) return <div className="text-center w-100"><FontAwesomeIcon icon={faSpinner} spin /></div>;

  return (
    <div>
      <table className="table">
        <thead>
          <tr>
            {table.columns.map((col) => (
              <td key={col.index} onClick={() => sortTable(col.index)}>
                {col.name}
								{col.index === sortColumnIndex && <FontAwesomeIcon className="ms-2" icon={faChevronDown} size="sm" />}
								<a href={`Columns/DeleteColumn?tableIndex=${tableIndex}&columnIndex=${col.index}`}>
									<FontAwesomeIcon className="float-end me-2 mt-1 text-danger" icon={faX} size="sm" />
								</a>
              </td>
            ))}
            <td className="text-end">Actions</td>
          </tr>
        </thead>
        <tbody>{table.rows.map((row) => getRow(row))}</tbody>
      </table>
    </div>
  );
};
