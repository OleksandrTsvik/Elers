import { CloseOutlined } from '@ant-design/icons';
import { App, Button, Tooltip, Typography } from 'antd';
import { useTranslation } from 'react-i18next';

import { useDeleteCourseTabMutation } from '../../../api/courses.api';
import useDisplayError from '../../../hooks/use-display-error';
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

  const { modal } = App.useApp();
  const { displayError } = useDisplayError();

  const [deleteCourseTab] = useDeleteCourseTabMutation();

  const handleDeleteSection = async () => {
    await modal.confirm({
      title: t('course_edit_page.confirm_delete_section', {
        section: section.name,
      }),
      content: t('actions.confirm_delete'),
      okButtonProps: { danger: true },
      onOk: () =>
        deleteCourseTab({ id: section.id })
          .unwrap()
          .then(() => onDeleteSection(section))
          .catch((error) => displayError(error)),
    });
  };

  return (
    <Typography.Title level={3}>
      {section.name}
      <Tooltip title={t('course_edit_page.delete_section')}>
        <Button
          danger
          type="link"
          icon={<CloseOutlined />}
          onClick={handleDeleteSection}
        />
      </Tooltip>
    </Typography.Title>
  );
}
