import { List } from 'antd';

import HomeCourseListItem from './home.course-list-item';
import { useGetListCoursesQuery } from '../../api/courses.api';
import usePagination from '../../hooks/use-pagination';

interface Props {
  search?: string;
}

export default function HomeCourseList({ search }: Props) {
  const { pagination, pagingParams } = usePagination({ pageSize: 10 });

  const { data, isFetching } = useGetListCoursesQuery({
    ...pagingParams,
    search,
  });

  return (
    <List
      size="large"
      itemLayout="vertical"
      loading={isFetching}
      dataSource={data?.items}
      pagination={pagination(data?.pageSize, data?.totalCount)}
      renderItem={(item) => <HomeCourseListItem courseItem={item} />}
    />
  );
}
