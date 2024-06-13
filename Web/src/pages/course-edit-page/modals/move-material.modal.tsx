import { Button, Flex, Modal, Spin } from 'antd';
import { useTranslation } from 'react-i18next';

import { CourseEditModalMode } from './edit-modal-mode.enum';
import { useMoveMaterialToAnotherTabMutation } from '../../../api/course-materials.mutations.api';
import { ErrorAlert } from '../../../common/error';
import { CourseTab } from '../../../models/course-tab.interface';
import useCourseEditState from '../use-course-edit.state';

interface Props {
  courseTabs: CourseTab[];
}

export default function MoveMaterialModal({ courseTabs }: Props) {
  const { t } = useTranslation();

  const { activeCourseMaterial, modalMode, onCloseModal } =
    useCourseEditState();

  const [moveMaterialToAnotherTab, { isLoading, error }] =
    useMoveMaterialToAnotherTabMutation();

  const handleMoveClick = async (newCourseTabId: string) => {
    if (!activeCourseMaterial) {
      return;
    }

    await moveMaterialToAnotherTab({
      materialId: activeCourseMaterial.id,
      newCourseTabId,
    })
      .unwrap()
      .then(() => onCloseModal());
  };

  return (
    <Modal
      destroyOnClose
      open={modalMode === CourseEditModalMode.MoveMaterial}
      title={t('course_edit_page.move_to_another_section')}
      footer={null}
      onCancel={onCloseModal}
    >
      <ErrorAlert className="mb-field" error={error} />

      <Spin spinning={isLoading}>
        <Flex gap="middle" vertical>
          {courseTabs.map(({ id, name, color }) => (
            <Button
              key={id}
              disabled={activeCourseMaterial?.courseTabId === id}
              style={{ whiteSpace: 'normal', height: 'auto', color }}
              onClick={() => handleMoveClick(id)}
            >
              {name}
            </Button>
          ))}
        </Flex>
      </Spin>
    </Modal>
  );
}
