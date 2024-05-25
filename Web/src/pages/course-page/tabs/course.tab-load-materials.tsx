import { Skeleton } from 'antd';

import CourseTabContent from './course.tab-content';
import { useGetListCourseMaterialsByTabIdQuery } from '../../../api/course-materials.api';

interface Props {
  tabId: string;
}

export default function CourseTabLoadMaterials({ tabId }: Props) {
  const { data, isFetching } = useGetListCourseMaterialsByTabIdQuery({
    id: tabId,
  });

  if (isFetching) {
    return <Skeleton active />;
  }

  return <CourseTabContent materials={data ?? []} />;
}
