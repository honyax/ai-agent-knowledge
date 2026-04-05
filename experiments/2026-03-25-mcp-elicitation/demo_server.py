"""
MCP Elicitation デモサーバー

2つのツールで Elicitation の基本動作を確認する:
  1. echo_with_input  - テキスト入力を受け取ってオウム返しする（最小構成）
  2. create_task      - タイトル・優先度・確認の3フィールドを収集する（構造化入力）

起動方法:
  python demo_server.py

Claude Code への登録（プロジェクトの .claude/settings.json に追記）:
  {
    "mcpServers": {
      "elicitation-demo": {
        "command": "python",
        "args": ["D:/var/git/ai-agent-knowledge/experiments/2026-03-25-mcp-elicitation/demo_server.py"]
      }
    }
  }

呼び出し方（Claude Code のチャットで）:
  「echo_with_input ツールを使って」
  「create_task ツールでタスクを作って」
"""

from dataclasses import dataclass
from typing import Literal

from fastmcp import FastMCP, Context

mcp = FastMCP("Elicitation Demo")


# -------------------------------------------------------
# Demo 1: テキスト1フィールドだけの最小構成
# -------------------------------------------------------

@dataclass
class TextInput:
    message: str


@mcp.tool
async def echo_with_input(ctx: Context) -> str:
    """ユーザーからテキスト入力を受け取り、オウム返しする。Elicitation の最小デモ。"""
    result = await ctx.elicit(
        message="何か入力してください（オウム返しします）",
        response_type=TextInput,
    )

    if result.action == "accept":
        return f"あなたが入力したのは: 「{result.data.message}」"
    elif result.action == "decline":
        return "入力が拒否されました"
    else:
        return "キャンセルされました"


# -------------------------------------------------------
# Demo 2: 複数フィールド + enum の構造化入力
# -------------------------------------------------------

@dataclass
class TaskInput:
    title: str
    priority: Literal["low", "medium", "high"]
    confirmed: bool


@mcp.tool
async def create_task(ctx: Context) -> str:
    """タスクのタイトル・優先度・確認をElicitationで収集し、タスクを作成する。"""
    result = await ctx.elicit(
        message="作成するタスクの情報を入力してください",
        response_type=TaskInput,
    )

    if result.action != "accept":
        return "タスク作成をキャンセルしました"

    task = result.data
    if not task.confirmed:
        return "確認がなかったためタスク作成を中断しました"

    return (
        f"タスクを作成しました!\n"
        f"  タイトル: {task.title}\n"
        f"  優先度: {task.priority}\n"
    )


if __name__ == "__main__":
    mcp.run()
