import { CloseOutlined } from '@ant-design/icons';
import { Button, Tooltip, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { CourseTab } from '../../../models/course.interface';

interface Props {
  section: CourseTab;
  onUpdateSection: (section: CourseTab) => void;
  onDeleteSection: (section: CourseTab) => void;
}

export default function SectionListItemTitle({
  section,
  onDeleteSection,
}: Props) {
  const { t } = useTranslation();

  return (
    <Typography.Title level={3}>
      {section.name}
      <Tooltip title={t('course_edit_page.delete_section')}>
        <Button
          danger
          type="link"
          icon={<CloseOutlined />}
          onClick={() => onDeleteSection(section)}
        />
      </Tooltip>
    </Typography.Title>
  );
}
