import { TableProps } from 'antd';
import { useState } from 'react';

import { SortParams } from '../common/types';

export default function useSortParams(initSortParams?: SortParams) {
  const [sortParams, setSortParams] = useState(initSortParams);

  const updateTableSortParams: TableProps['onChange'] = (_, __, sorter) => {
    if (Array.isArray(sorter)) {
      return;
    }

    const sortOrder = sorter.order;
    const sortColumn = sortOrder ? (sorter.columnKey as string) : undefined;

    setSortParams({ sortColumn, sortOrder });
  };

  return { sortParams, updateTableSortParams };
}
