﻿# LuanAVGEngine

简易的图形化文字AVG(Galgame)制作引擎，为本人本科C#程序设计课程大作业。项目功能基本可用，但目前不具备实际Galgame制作使用价值。

.NET平台下较优秀的Galgame制作引擎可参考YuriAVGEngine(https://github.com/rinkako/YuriAVGEngine)



项目支持通过纯声明式脚本定义游戏剧情系统。项目以自定义的脚本为中心，提供剧本文件与脚本的转换，实现了文字AVG运行时环境，通过解释脚本读入素材展示游戏界面，通过脚本指令对游戏符号状态的控制及指令跳转机制实现文字AVG游戏中剧本控制全部逻辑的基本实现。通过纯脚本的信号控制机制实现有限的自定义游戏系统的实现。

### 基本信息

项目开发环境为Visual Studio 2017

基于.NET 4.6.1开发

### 项目设计

#### 项目结构

LuanCore：脚本对应的内存类。

LuanEditor：可视化脚本编辑器。

LuanPlatform：虚拟机，逐行解释脚本并作出相应动作。

#### 组织概念

![Concept](https://github.com/sutakori/LuanAVGEngine/blob/master/concepts.png)

剧本：剧作者编辑剧本文件，一个剧本一个文件，以文件名标识剧本。剧本转化为脚本供游戏制作后续工作展开（由于一些使用上的概念不清晰，该功能的支持暂未纳入版本管理）

脚本：项目核心概念，分为媒体元素指令与控制指令。指令可携带赋值块，当虚拟机执行到该命令时，或触发了某些操作后（如选择支中的按钮赋值块在选定之后执行）执行，通过对变量的赋值与计算改变游戏状态。

```reStructuredText
脚本 -> {指令}*

指令 -> @指令名 {键=”值”}* {指令}* {赋值块} \n

赋值块 -> \{{变量=表达式;}*\}
```

Core：脚本对应的内存对象，导入脚本文件时直接将其编译为Section对象。

Section：章节类，一个章节对应一个脚本文件，相应的对应一个剧本，为第一级剧本组织概念。

Scene：场景类，次级剧本组织概念，一个Section由多个Scene组成。

Instruction：命令类，一句脚本命令对应一个命令。

#### 演绎概念

RuntimeManager：游戏运行过程中全部状态的描述，包括游戏变量状态、游戏脚本位置状态和演绎状态。

Frame：游戏演绎状态，描述场上存在的资源及其位置等。脚本对媒体元素的控制表现为Frame的转变及其中间过程。以文本命令为例，文本的切换表现为文本框开始展示打字动画，并开始播放配音。点击屏幕进入稳定状态，即停止打字动画，显示指示倒三角，并停止配音。

