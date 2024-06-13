import { Skeleton, Tabs } from 'antd';

import MyProgressHead from './my-progress.head';
import MyProgressTab from './my-progress.tab';
import { useGetMyEnrolledCoursesQuery } from '../../api/profile.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MyProgressPage() {
  const { data, isFetching, error } = useGetMyEnrolledCoursesQuery();

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MyProgressHead />
      <Tabs
        items={data.map(({ id, title }) => ({
          key: id,
          label: title,
          children: <MyProgressTab courseId={id} />,
        }))}
      />
    </>
  );
}
