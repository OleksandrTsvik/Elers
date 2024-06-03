import { PaginationProps } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import { PagingParams } from '../common/types';

export default function usePagination(initPagingParams?: PagingParams): {
  pagingParams?: PagingParams;
  pagination: (pageSize?: number, totalCount?: number) => PaginationProps;
} {
  const { t } = useTranslation();

  const [pagingParams, setPagingParams] = useState(initPagingParams);

  return {
    pagingParams,
    pagination: (pageSize, totalCount) => ({
      responsive: true,
      showSizeChanger: true,
      defaultPageSize: 20,
      pageSizeOptions: [5, 10, 20, 50, 100],
      pageSize,
      total: totalCount,
      showTotal: (total) => t('pagination.total', { total }),
      onChange: (page, pageSize) =>
        setPagingParams({ pageNumber: page, pageSize }),
    }),
  };
}
