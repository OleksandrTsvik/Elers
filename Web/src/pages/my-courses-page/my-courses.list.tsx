import { List } from 'antd';

import MyCoursesListItem from './my-courses.list-item';
import { useGetMyCoursesQuery } from '../../api/courses.api';
import usePagination from '../../hooks/use-pagination';

export default function MyCoursesList() {
  const { pagination, pagingParams } = usePagination({ pageSize: 20 });

  const { data, isFetching } = useGetMyCoursesQuery({ ...pagingParams });

  return (
    <List
      grid={{
        gutter: 16,
        xs: 1,
        sm: 2,
        md: 3,
        lg: 3,
        xl: 4,
        xxl: 4,
      }}
      loading={isFetching}
      dataSource={data?.items}
      pagination={pagination(data?.pageSize, data?.totalCount)}
      renderItem={(item) => <MyCoursesListItem item={item} />}
    />
  );
}
