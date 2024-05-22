import TabContentByType from './tab.content-by-type';
import TabContentEditPanel from './tab.content-edit-panel';
import { CourseMaterial } from '../../../models/course-material.type';
import { classnames } from '../../../utils/helpers';
import MaterialCreationButton from '../material-creation/material-creation.button';

interface Props {
  tabId: string;
  materials: CourseMaterial[];
}

export default function TabContent({ tabId, materials }: Props) {
  return (
    <>
      {materials.map((item) => (
        <div
          key={item.id}
          className={classnames('d-block', 'mt-course_material')}
        >
          <TabContentEditPanel material={item} />
          <TabContentByType material={item} />
        </div>
      ))}
      <MaterialCreationButton tabId={tabId} />
    </>
  );
}
