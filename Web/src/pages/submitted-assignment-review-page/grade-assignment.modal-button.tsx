import { Button, FormInstance, Modal } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import GradeAssignmentForm, { GradeFormValues } from './grade-assignment.form';
import { useGradeAssignmentMutation } from '../../api/assignments.api';
import useModal from '../../hooks/use-modal';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

interface Props {
  submittedAssignmentId: string;
  grade?: number;
  comment?: string;
  maxGrade: number;
}

export default function GradeAssignmentModalButton({
  submittedAssignmentId,
  grade,
  comment,
  maxGrade,
}: Props) {
  const { t } = useTranslation();
  const { isOpen, onClose, onOpen } = useModal();

  const [formInstance, setFormInstance] =
    useState<FormInstance<GradeFormValues>>();

  const [gradeAssignment, { isLoading, error }] = useGradeAssignmentMutation();

  const handleSubmit = async (values: GradeFormValues) => {
    await gradeAssignment({ submittedAssignmentId, ...values }).unwrap();
  };

  return (
    <>
      <Button type="primary" onClick={onOpen}>
        {t('submitted_task_review_page.give_grade')}
      </Button>
      <Modal
        destroyOnClose
        open={isOpen}
        confirmLoading={isLoading}
        title={t('submitted_task_review_page.setting_grade')}
        onOk={formInstance?.submit}
        onCancel={onClose}
      >
        <GradeAssignmentForm
          maxGrade={maxGrade}
          initialValues={{
            status: SubmittedAssignmentStatus.Graded,
            grade: grade ?? 0,
            comment,
          }}
          error={error}
          onSubmit={handleSubmit}
          onFormInstanceReady={setFormInstance}
        />
      </Modal>
    </>
  );
}
