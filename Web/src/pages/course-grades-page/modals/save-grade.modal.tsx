import { InputNumber, Modal, Typography } from 'antd';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { GradesModalMode } from './grades-modal-mode.enum';
import {
  useCreateGreadMutation,
  useUpdateGreadMutation,
} from '../../../api/grades.api';
import { ErrorAlert } from '../../../common/error';
import { UserAvatar } from '../../../components';
import { stringToInputNumber } from '../../../utils/helpers';
import useCourseGradesState from '../use-course-grades.state';

export default function SaveGradeModal() {
  const { t } = useTranslation();

  const { modalMode, activeGradeCell, onCloseModal } = useCourseGradesState();
  const [grade, setGrade] = useState<number>(activeGradeCell?.grade ?? 0);

  const [createGread, { isLoading: isLoadingCreate, error: errorCreate }] =
    useCreateGreadMutation();

  const [updateGread, { isLoading: isLoadingUpdate, error: errorUpdate }] =
    useUpdateGreadMutation();

  useEffect(() => {
    setGrade(activeGradeCell?.grade ?? 0);
  }, [activeGradeCell?.grade]);

  const handleSubmit = async () => {
    if (!activeGradeCell) {
      return;
    }

    if (activeGradeCell.gradeId) {
      await updateGread({ gradeId: activeGradeCell.gradeId, value: grade })
        .unwrap()
        .then(() => onCloseModal());
    } else {
      await createGread({
        studentId: activeGradeCell.student.id,
        assessmentId: activeGradeCell.assessmentId,
        gradeType: activeGradeCell.gradeType,
        value: grade,
      })
        .unwrap()
        .then(() => onCloseModal());
    }
  };

  return (
    <Modal
      open={modalMode === GradesModalMode.SaveGrade}
      confirmLoading={isLoadingCreate || isLoadingUpdate}
      title={activeGradeCell?.title}
      okText={t('actions.save_changes')}
      onOk={handleSubmit}
      onCancel={onCloseModal}
    >
      {activeGradeCell?.student && (
        <>
          <Typography.Paragraph type="secondary">
            {t('user.student')}:
          </Typography.Paragraph>
          <Typography.Paragraph>
            <UserAvatar
              className="mr-avatar"
              url={activeGradeCell.student.avatarUrl}
            />
            {activeGradeCell.student.lastName}{' '}
            {activeGradeCell.student.firstName}{' '}
            {activeGradeCell.student.patronymic}
          </Typography.Paragraph>
        </>
      )}

      <ErrorAlert error={errorCreate || errorUpdate} />

      <Typography.Paragraph className="mb-0 mt-field pb-label-field">
        {t('course_grades_page.grade')}:
      </Typography.Paragraph>

      <InputNumber
        className="w-100 mb-field"
        value={grade}
        min={0}
        max={activeGradeCell?.maxGrade}
        addonAfter={t('course_material.max_grade_tip', {
          maxGrade: activeGradeCell?.maxGrade,
        })}
        parser={(displayValue) =>
          stringToInputNumber(displayValue, true, true, 0)
        }
        onChange={(value) => value && setGrade(value)}
      />
    </Modal>
  );
}
