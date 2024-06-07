import { Form, FormInstance, Input, InputNumber, Radio } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import useGradeAssignmentRules from './use-grade-assignment.rules';
import { ErrorForm } from '../../common/error';
import { ASSIGNMENT_RULES } from '../../common/rules';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';
import { stringToInputNumber } from '../../utils/helpers';

export interface GradeFormValues {
  status: SubmittedAssignmentStatus;
  grade: number;
  comment?: string;
}

interface Props {
  maxGrade: number;
  initialValues: GradeFormValues;
  error: unknown;
  onSubmit: (values: GradeFormValues) => Promise<void>;
  onFormInstanceReady: (instance: FormInstance<GradeFormValues>) => void;
}

export default function GradeAssignmentForm({
  maxGrade,
  initialValues,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<GradeFormValues>();
  const rules = useGradeAssignmentRules(maxGrade);

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={onSubmit}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="status"
        label={t('assignment.status')}
        rules={rules.status}
      >
        <Radio.Group>
          <Radio value={SubmittedAssignmentStatus.Graded}>
            {t('submitted_task_review_page.grade_status.graded')}
          </Radio>
          <Radio value={SubmittedAssignmentStatus.Resubmit}>
            {t('submitted_task_review_page.grade_status.resubmit')}
          </Radio>
        </Radio.Group>
      </Form.Item>

      <Form.Item
        hasFeedback
        name="grade"
        label={t('assignment.grade')}
        rules={rules.grade}
      >
        <InputNumber
          className="w-100"
          addonAfter={t('course_material.max_grade_tip', { maxGrade })}
          min={0}
          max={maxGrade}
          parser={(displayValue) => stringToInputNumber(displayValue)}
        />
      </Form.Item>

      <Form.Item
        name="comment"
        label={t('assignment.comment')}
        rules={rules.comment}
      >
        <Input.TextArea
          showCount
          maxLength={ASSIGNMENT_RULES.comment.max}
          autoSize={{ minRows: 3, maxRows: 6 }}
        />
      </Form.Item>
    </Form>
  );
}
