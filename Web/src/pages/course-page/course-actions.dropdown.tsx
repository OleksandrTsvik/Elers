import Icon from '@ant-design/icons';
import { Dropdown, Button, Space } from 'antd';
import { IoMdArrowDropdown } from 'react-icons/io';
import { IoSettingsSharp } from 'react-icons/io5';

import useCourseActions from './use-course.actions';

interface Props {
  courseId: string;
}

export default function CourseActionsDropdown({ courseId }: Props) {
  const { getActionItems } = useCourseActions();

  return (
    <Dropdown menu={{ items: getActionItems(courseId) }} trigger={['click']}>
      <Button type="link">
        <Space>
          <Icon component={IoSettingsSharp} />
          <Icon component={IoMdArrowDropdown} />
        </Space>
      </Button>
    </Dropdown>
  );
}
