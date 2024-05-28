import { Skeleton } from 'antd';

import TabContent from './tab.content';
import { useGetListCourseMaterialsByTabIdToEditQuery } from '../../../api/course-materials.queries.api';

interface Props {
  tabId: string;
}

export default function TabLoadMaterials({ tabId }: Props) {
  const { data, isFetching } = useGetListCourseMaterialsByTabIdToEditQuery({
    id: tabId,
  });

  if (isFetching) {
    return <Skeleton active />;
  }

  return <TabContent tabId={tabId} materials={data ?? []} />;
}
