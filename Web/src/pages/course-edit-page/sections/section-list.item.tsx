import { Divider, Typography } from 'antd';

import { CourseTab } from '../../../models/course.interface';
import TabContent from '../tabs/tab.content';
import TabSettingsDropdown from '../tabs/tab.settings-dropdown';

interface Props {
  section: CourseTab;
}

export default function SectionListItem({ section }: Props) {
  return (
    <>
      <Divider />
      <Typography.Title level={3}>
        {section.name}
        <TabSettingsDropdown courseTab={section} />
      </Typography.Title>
      <TabContent tab={section} />
    </>
  );
}
