import { Skeleton } from 'antd';

import TabContent from './tab.content';
import { useGetListCourseMaterialsByTabIdQuery } from '../../../api/course-materials.api';

interface Props {
  tabId: string;
}

export default function TabLoadMaterials({ tabId }: Props) {
  const { data, isFetching } = useGetListCourseMaterialsByTabIdQuery({
    id: tabId,
  });

  if (isFetching) {
    return <Skeleton active />;
  }

  return <TabContent tabId={tabId} materials={data ?? []} />;
}
